import sys
import librosa
import numpy as np
import constants as c
from scipy.signal import lfilter
import sigproc
import traceback

def normalize_frames(m,epsilon=1e-12):
    return np.array([(v - np.mean(v)) / max(np.std(v),epsilon) for v in m])

# https://github.com/christianvazquez7/ivector/blob/master/MSRIT/rm_dc_n_dither.m
def remove_dc_and_dither(sin, sample_rate):
    if sample_rate == 16e3:
        alpha = 0.99
    elif sample_rate == 8e3:
        alpha = 0.999
    else:
        print("Sample rate must be 16kHz or 8kHz only")
        exit(1)
    sin = lfilter([1,-1], [1,-alpha], sin)
    dither = np.random.random_sample(len(sin)) + np.random.random_sample(len(sin)) - 1
    spow = np.std(dither)
    sout = sin + 1e-6 * spow * dither
    return sout

def build_buckets(max_sec, step_sec, frame_step):
    buckets = {}
    frames_per_sec = int(1/frame_step)
    end_frame = int(max_sec*frames_per_sec)
    step_frame = int(step_sec*frames_per_sec)
    for i in range(0, end_frame+1, step_frame):
        s = i
        s = np.floor((s-7+2)/2) + 1  # conv1
        s = np.floor((s-3)/2) + 1  # mpool1
        s = np.floor((s-5+2)/2) + 1  # conv2
        s = np.floor((s-3)/2) + 1  # mpool2
        s = np.floor((s-3+2)/1) + 1  # conv3
        s = np.floor((s-3+2)/1) + 1  # conv4
        s = np.floor((s-3+2)/1) + 1  # conv5
        s = np.floor((s-3)/2) + 1  # mpool5
        s = np.floor((s-1)/1) + 1  # fc6
        if s > 0:
            buckets[i] = int(s)
    return buckets

def get_fft_spectrum(signal, buckets):

    signal *= 2**15

    # get FFT spectrum
    signal = remove_dc_and_dither(signal, c.SAMPLE_RATE)
    signal = sigproc.preemphasis(signal, coeff=c.PREEMPHASIS_ALPHA)
    frames = sigproc.framesig(signal, frame_len=c.FRAME_LEN*c.SAMPLE_RATE, frame_step=c.FRAME_STEP*c.SAMPLE_RATE, winfunc=np.hamming)
    fft = abs(np.fft.fft(frames,n=c.NUM_FFT))
    fft_norm = normalize_frames(fft.T)

    # truncate to max bucket sizes
    rsize = max(k for k in buckets if k <= fft_norm.shape[1])
    rstart = int((fft_norm.shape[1]-rsize)/2)
    out = fft_norm[:,rstart:rstart+rsize]

    return out


if __name__ == '__main__':
    try:
        np.set_printoptions(threshold=sys.maxsize)
        path_with_voice = sys.argv[1]
        with open(path_with_voice, "r") as f:
            voice_from_cmd = f.readline().strip('[]').split(' ')
        voice = np.asarray([float(e) for e in voice_from_cmd])
        buckets = build_buckets(3, c.BUCKET_STEP, c.FRAME_STEP)
        print(get_fft_spectrum(voice, buckets))
    except Exception as e:
        print(traceback.format_exc())
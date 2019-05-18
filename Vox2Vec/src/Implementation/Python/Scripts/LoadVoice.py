import sys
import librosa
import numpy

def load_wav(filename, sample_rate):
    audio, sr = librosa.load(filename, sr=sample_rate, mono=True)
    audio = audio.flatten()
    return audio
	
if __name__ == '__main__':
    numpy.set_printoptions(threshold=sys.maxsize)
    loaded = load_wav(sys.argv[1], 16000)
    print(loaded)
﻿using Vox2Vec.Models;

namespace Vox2Vec.Services
{
    public interface IVoicePreprocessor
    {
        Vox PreprocessVoice(Voice voice);
    }
}
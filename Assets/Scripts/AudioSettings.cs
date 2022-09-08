using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class AudioSettings
    {
        public float MainAudioVolume { get; set; }
        public float AudioEffectsVolume { get; set; }

        public AudioSettings() 
        {
            MainAudioVolume = 1;
            AudioEffectsVolume = 1;
        }

        public AudioSettings(float MainAudioVolume, float AudioEffectsVolume)
        {
            this.MainAudioVolume = MainAudioVolume;
            this.AudioEffectsVolume = AudioEffectsVolume;
        }

        public void Deserialize()
        {
            bool isFileFound = false;

            DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath + "/Saves/");
            foreach (var file in directoryInfo.GetFiles())
            {
                if (file.Name == "AudioSettings.xml")
                {
                    isFileFound = true;
                    break;
                }
            }

            if (isFileFound)
            {

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(AudioSettings));
                AudioSettings audioSettings;

                using (FileStream fs = new FileStream(Application.dataPath + "/Saves/AudioSettings.xml", FileMode.Open))
                {
                    audioSettings = (xmlSerializer.Deserialize(fs) as AudioSettings);
                }

                MainAudioVolume = audioSettings.MainAudioVolume;
                AudioEffectsVolume = audioSettings.AudioEffectsVolume;
            }
            else
            {
                Serialize();
            }
        }

        public void Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(AudioSettings));

            using (FileStream fs = new FileStream(Application.dataPath + "/Saves/AudioSettings.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, this);
            }
        }

        public void ChangeAudioEffectsVolume(float volume)
        {
            AudioEffectsVolume = volume;
            Serialize();
        }

        public void ChangeMainAudioVolume(float volume)
        {
            MainAudioVolume = volume;
            Serialize();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

namespace DBGads
{
    public enum ADS_CONSENT
    {
        relevant,
        random,
        none
    }

    [Serializable]
    public class AdSDB
    {
        public ADS_CONSENT consent;

        public AdSDB(ADS_CONSENT consent)
        {
            this.consent = consent;
        }

        public static void Delete()
        {
            string path = Application.persistentDataPath + "/Ads";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void Save(ADS_CONSENT consent)
        {
            AdSDB ads = new AdSDB(consent);

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/Ads");
            bf.Serialize(file, ads);
            file.Close();
        }

        public static AdSDB Get()
        {
            if (File.Exists(Application.persistentDataPath + "/Ads"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/Ads", FileMode.Open);
                AdSDB ads = (AdSDB)bf.Deserialize(file);
                file.Close();
                return ads;
            }
            return null;
        }
    }
}

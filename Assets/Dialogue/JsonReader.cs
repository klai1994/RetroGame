using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;
using System.IO;

namespace Game.Dialogue
{
    public class JsonReader
    {
        public static Dictionary<int, string> localPathTable = new Dictionary<int, string>
            {
                {1, "/Dialogue/TestEvent.json" }
            };

        public static DialogueEventHolder ConvertJsonToDialogueEvent(int filePathId)
        {
            string localPath = localPathTable[filePathId];
            string jsonContent = File.ReadAllText(Application.dataPath + localPath);

            return JsonMapper.ToObject<DialogueEventHolder>(jsonContent);
        }

    }
}
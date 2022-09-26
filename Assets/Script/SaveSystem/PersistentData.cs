using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystemCore
{
    
    public static class PersistentData
    {
        public static bool firstRunDone = false;
        public static List<int> lastTwentyScores = new List<int>();
        public static bool gameSound;
        public static bool gameMusic;

        //blockchain data
        public static int tokenCount;
    }
}
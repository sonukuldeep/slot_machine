using System.Collections;
using UnityEngine;


namespace GameScripts
{
    public class Row : MonoBehaviour
    {
        private int randomValue;
        private float timeInterval;

        private bool rowStopped;
        private int stoppedSlot;

        private void Start()
        {
            rowStopped = true;
            GameControls.HandlePulled += StartRotating;
        }

        public bool RowStopped
        {
            get { return rowStopped; }
        }

        public int StoppedSlot
        {
            get { return stoppedSlot; }
        }

        private void StartRotating()
        {
            stoppedSlot = 0;
            StartCoroutine("Rotate");
        }

        private IEnumerator Rotate()
        {
            rowStopped = false;
            timeInterval = 0.025f;

            for (int i = 0; i < 30; i++)
            {
                if (transform.position.y <= -3.5f) transform.position = new Vector2(transform.position.x, 1.75f);

                transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);
                yield return new WaitForSeconds(timeInterval);
            }

            randomValue = Random.Range(60, 100);
                        
            if (randomValue % 3 == 1 || randomValue % 3 == 2) randomValue += 3 - (randomValue % 3);
            
            for (int i = 0; i < randomValue; i++)
            {
                if(transform.position.y <= -3.5f) transform.position = new Vector2(transform.position.x, 1.75f);

                transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);
                if (i > Mathf.RoundToInt(randomValue * 0.25f)) timeInterval = 0.05f;
                if (i > Mathf.RoundToInt(randomValue * 0.5f)) timeInterval = 0.1f;
                if (i > Mathf.RoundToInt(randomValue * 0.75f)) timeInterval = 0.15f;
                if (i > Mathf.RoundToInt(randomValue * 0.95f)) timeInterval = 0.2f;

                yield return new WaitForSeconds(timeInterval);
            }

            switch (transform.position.y)
            {
                case -3.5f:
                    stoppedSlot = 1; // Diamond
                    rowStopped = true;
                    break;
                case -2.75f:
                    stoppedSlot = 2; // Crown
                    rowStopped = true;
                    break;
                case -2f:
                    stoppedSlot = 3; // Melon
                    rowStopped = true;
                    break;
                case -1.25f:
                    stoppedSlot = 4; // Bar
                    rowStopped = true;
                    break;
                case -0.5f:
                    stoppedSlot = 5; // Seven
                    rowStopped = true;
                    break;
                case 0.25f:
                    stoppedSlot = 6; // Cherry
                    rowStopped = true;
                    break;
                case 1f:
                    stoppedSlot = 7; // Lemon
                    rowStopped = true;
                    break;
                case 1.75f:
                    stoppedSlot = 1; // Diamond
                    rowStopped = true;
                    break;
            }

        
        }

        private void OnDestroy()
        {
            GameControls.HandlePulled -= StartRotating;
        }
    }
}

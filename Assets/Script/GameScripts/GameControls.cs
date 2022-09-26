using System.Collections;
using UnityEngine;
using System;
using TMPro;
using SaveSystemCore;
using Audio;
using Blockchain;

namespace GameScripts
{
    public class GameControls : MonoBehaviour
    {
        public static event Action HandlePulled = delegate { };

        [SerializeField] TMP_Text prizeText;
        [SerializeField] Row[] rows;
        [SerializeField] Transform handle;
        private int prizeValue = 0;
        private bool resultsChecked;
        
        //Audio manager
        private AudioManager audioManager;

        //blockchain
        [SerializeField] TMP_Text ticketCount;
        private bool taskDone = false;

        public int PrizeValue { get { return prizeValue; } } //sending PrizeValue to PlayerData


        private void Start()
        {
            resultsChecked = true;
            audioManager = FindObjectOfType<AudioManager>();

            myfun();
            
        }

        private async void myfun()
        {
            GetTickets getTicket = new GetTickets();
            int ticket = await getTicket.TokenBalance();
            taskDone = true;
            PersistentData.tokenCount = ticket;
        }

        private void Update()
        {
            if(rows[0].RowStopped && rows[1].RowStopped && rows[2].RowStopped && !resultsChecked)
            {
                CheckResults();
                prizeText.text = "Prize: " + prizeValue;
                prizeText.enabled = true;
                resultsChecked = true;
                
                //adding value to persistant file
                if (PersistentData.lastTwentyScores.Count > 20) PersistentData.lastTwentyScores.RemoveAt(0);
                PersistentData.lastTwentyScores.Add(prizeValue);
            }
            
            ticketCount.text = PersistentData.tokenCount.ToString();
        }
        
        

        private void OnMouseDown()
        {
            if (!taskDone)
            {
                Debug.Log("wait a minute!");
                return;
            }

            if (rows[0].RowStopped && rows[1].RowStopped && rows[2].RowStopped)
            {
                StartCoroutine("PullHandle");
                StartCoroutine(audioManager.Play("crank",1f));
            }
            prizeText.enabled = false;
            
            //reset to false
            taskDone = false;
            myfun();
        }

        private IEnumerator PullHandle()
        {

            for (int i = 0; i < 15; i++)
            {
                handle.Rotate(0, 0, i);
                yield return new WaitForSeconds(0.1f);
            }

            HandlePulled();

            for (int i = 0; i < 15; i++)
            {
                handle.Rotate(0, 0, -i);
                yield return new WaitForSeconds(0.1f);
            }

            resultsChecked = false;
            
        }

        private void CheckResults()
        {
            //1 is Diamond
            //2 is Crown
            //3 is Melon
            //4 is Bar
            //5 is Seven
            //6 is Cherry
            //7 is Lemon

            if(rows[0].StoppedSlot == rows[1].StoppedSlot && rows[0].StoppedSlot == rows[2].StoppedSlot)
            {
                switch(rows[0].StoppedSlot + rows[1].StoppedSlot + rows[2].StoppedSlot)
                {
                    case 3: // all diamand
                        prizeValue = 200;
                        return;

                    case 6: // all Crown
                        prizeValue = 400;
                        return;

                    case 9: // all melon
                        prizeValue = 600;
                        return;

                    case 12: // all bar
                        prizeValue = 800;
                        return;

                    case 15: // all seven
                        prizeValue = 1000;
                        return;

                    case 18: // all cherry
                        prizeValue = 1200;
                        return;

                    case 21: // all lemon
                        prizeValue = 1400;
                        return;
                }

            }

            if (rows[0].StoppedSlot == rows[1].StoppedSlot || rows[0].StoppedSlot == rows[2].StoppedSlot || rows[1].StoppedSlot == rows[2].StoppedSlot)
            {
                if (rows[0].StoppedSlot == rows[1].StoppedSlot || rows[0].StoppedSlot == rows[2].StoppedSlot)
                {
                    prizeValue = rows[0].StoppedSlot * 10;
                }
                else prizeValue = rows[1].StoppedSlot * 10;
            }
            else prizeValue = 0;
            
        }
    }
}

using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Blockchain
{
    public class GetTickets
    {
        //private int ticket;

        public async Task<int> TokenBalance()
        {
            string chain = "ethereum";
            string network = "mainnet";
            string contract = "0x60f80121c31a0d46b5279700f9df786054aa5ee5";
            string account = "0x6b2be2106a7e883f282e2ea8e203f516ec5b77f7";

            int balance = await ERC721.BalanceOf(chain, network, contract, account);
            return balance; 
        }

        
        //public int TicketBalance { get { return ticket; } }
    }
}

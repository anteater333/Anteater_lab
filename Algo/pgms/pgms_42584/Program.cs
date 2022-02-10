using System;
using System.Collections.Generic;

namespace pgms_42584
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] question = new int[] {5,4,3,2,1};

            int[] answer = new Solution().solution(question);

            foreach(var item in answer) {
                Console.Write(item.ToString() + ' ');
            }
        }
    }

    class Stock {
        public int Price { get => _price; }
        public int Time { get => _time; }

        private int _price;
        private int _time;

        public Stock (int price, int time) {
            this._price = price;
            this._time = time;
        }
    }

    public class Solution {
        public int[] solution(int[] prices) {
            int[] answer = new int[prices.Length];
            Stack<Stock> stockStack = new Stack<Stock>();

            Stock loser;

            int max = prices[0];
            stockStack.Push(new Stock(prices[0], 0));

            for (int index = 1; index < prices.Length; index++) {
                foreach(var item in stockStack) {
                    Console.Write(item.Price.ToString()+":"+item.Time.ToString()+", ");
                }
                Console.WriteLine();
                if ( prices[index] >= max ) {   //  우린 부자가 될 거야!
                    max = prices[index];
                    stockStack.Push(new Stock(prices[index], index));
                }
                else {
                    max = prices[index];
                    while ( stockStack.Count != 0 && stockStack.Peek().Price > max ) {
                        loser = stockStack.Pop();
                        answer[loser.Time] = index - loser.Time;
                    }
                    stockStack.Push(new Stock(prices[index], index));
                }
            }

            while (stockStack.Count > 0) {  // Stack 찌꺼기 처리
                loser = stockStack.Pop();
                answer[loser.Time] = (prices.Length - 1) - loser.Time;
            }

            return answer;
        }
    }
}
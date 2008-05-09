using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MemoryCards
{
    public class MemoryBoard : IDisposable
    {
        private SpriteBatch batch;
        private SpriteFont font;
        private Texture2D back;
        private Texture2D front;
        private List<Card> cards;

        private int cardHeight = 0;
        private int cardWidth = 0;
        private int leftMargin = 5;
        private int rightMargin = 5;
        private int topMargin = 5;
        private int bottomMargin = 5;
        private int noOfColumns = 5;

        private int card1, card2;

        public MemoryBoard(IEnumerable<string> tokens)
        {
            batch = null;
            font = null;
            cards = new List<Card>();
            foreach(string s in tokens)
            {
                cards.Add(new Card(s));
                cards.Add(new Card(s));
            }
            Shuffle(cards);
            card1 = card2 = cards.Count;
        }

        // Fisher-Yates Shuffle Algorithm
        public static void Shuffle( IList<Card> elements ) {
	        Random rng = new Random();
	        int n = elements.Count;
	        while( n > 1 ) {
		        int k = rng.Next(n);
		        --n;
		        Card temp = elements[n];
		        elements[n] = elements[k];
		        elements[k] = temp;
	        }
        }

        public SpriteBatch SpriteBatch
        {
            get
            {
                return batch;
            }
            set 
            {
                batch = value;
            }
        }

        public SpriteFont SpriteFont
        {
            get
            {
                return font;
            }
            set
            {
                font = value;
            }
        }

        public Texture2D Front
        {
            set
            {
                front = value;
                cardHeight = front.Height;
                cardWidth = front.Width;
            }
            get
            {
                return front;
            }
        }

        public Texture2D Back
        {
            set 
            {
                back = value;
            }
            get
            {
                return back;
            }
        }

        public void Draw()
        {
            batch.Begin(SpriteBlendMode.AlphaBlend);

            int i = 0;
            foreach (Card c in cards)
            {
                if (c.IsShowing)
                {
                    Color col = Color.Black;
                    if (c.IsSolved)
                    {
                        col = Color.Red;
                    }
                    batch.Draw(front, new Vector2(CalcXPosition(i), CalcYPosition(i)), Color.White);

                    batch.DrawString(font, c.Content, new Vector2(CalcXPosition(i) + 25.0f, CalcYPosition(i) + 50.0f), col);
                }
                else
                {
                    batch.Draw(back, new Vector2(CalcXPosition(i), CalcYPosition(i)), Color.White);
                }
                i++;
            }

            batch.End();
        }

        private float CalcXPosition(int i)
        {
            return (float)(i % noOfColumns) * cardWidth + leftMargin + (rightMargin * (i % noOfColumns));
        }

        private float CalcYPosition(int i)
        {
            return (float) (i / noOfColumns) * cardHeight + topMargin + (bottomMargin * (i/noOfColumns));
        }

        public void FlipCard(int mouseX, int mouseY)
        {
            // First if the second card isn't the cards.Count value, it means
            // both cards have been drawn and need to be flipped back
            if (card2 != cards.Count)
            {

                Flip(card1);
                Flip(card2);
                card1 = card2 = cards.Count;
            }
            int card = 0;
            while (card < cards.Count)
            {
                if (mouseX > CalcXPosition(card) && mouseX < CalcXPosition(card)+cardWidth &&
                    mouseY > CalcYPosition(card) && mouseY < CalcYPosition(card)+cardHeight &&
                    !cards[card].IsSolved)
                {
                    if (card1 == cards.Count)
                    {
                        card1 = card;
                    }
                    else if (card1 != card)
                    {
                        card2 = card;
                        if (cards[card1].Content.Equals(cards[card2].Content))
                        {
                            cards[card1].IsSolved = true;
                            cards[card2].IsSolved = true;
                            card1 = card2 = cards.Count;
                        }
                    }
                    else 
                    {
                        card1 = cards.Count;
                    }
                    Flip(card);
                    break;
                }
                card++;
            }
        }

        private void Flip(int cardNo)
        {
            cards[cardNo].IsShowing = !cards[cardNo].IsShowing;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (batch != null)
            {
                batch.Dispose();
            }
            if (back != null)
            {
                back.Dispose();
            }
            if (front != null)
            {
                front.Dispose();
            }
        }

        #endregion
    }
}

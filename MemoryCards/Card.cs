namespace MemoryCards
{
    public class Card
    {
        private string content;
        private bool isShowing;
        private bool isSolved;

        public Card(string content)
        {
            this.content = content;
            isShowing = false;
            isSolved = false;
        }

        public string Content
        {
            get
            {
                return content;
            }
        }

        public bool IsShowing
        {
            get
            {
                return isShowing;
            }
            set
            {
                isShowing = value;
            }
        }

        public bool IsSolved
        {
            get
            {
                return isSolved;
            }
            set
            {
                isSolved = value;
            }
        }
    }
}

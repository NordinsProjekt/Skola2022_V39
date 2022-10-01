namespace Lists
{
    public sealed class ListItem<T>
    {
        private T value;
        public ListItem<T> NextItem { get; set; }

        public ListItem(T value)
        {
            this.value = value;
        }
        public T Value { get { return value; } }
    }
}
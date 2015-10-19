namespace KANG.Common {
    public class MyLazy<T> where T : new() {
        private bool isLoaded;
        private T value;

        public MyLazy() {
            isLoaded = false;
        }

        public T Value {
            get {
                if (!isLoaded) {
                    value = new T();
                    isLoaded = true;
                }
                return value;
            }
        }
    }
}
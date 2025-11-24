using System.Text;

namespace SymbolTable {
    
}

namespace ListSymbolTable {
    public class ListSymbolTable<K, V> : IEnumerable<K> where K : IComparable<K> {

        public class MyNode<K, V> {
            public K key;
            public V value;
            public MyNode<K, V> next;
            public MyNode<K, V> prev;

            public MyNode(K key, V value = default) {
                this.key = key;
                this.value = value;
                next = null;
                prev = null;
            }

           

        }

        private MyNode<K, V> head;

        /* public ListSymbolTable(T[] array) {
            for (int i = 0; i < array.Length; i++) {
                this.Add(array[i]);
            }
        }
        */

        

        private int count = 0;

        public int Count {
            get {
                return count;
            }
        }
        public ListSymbolTable() {

            head = null;

        }

        /// <summary>
        /// Walks to a specified index in the list
        /// </summary>
        /// <param name="idx"></param>
        /// <returns>The node at the index</returns>
        private MyNode<K, V> WalkToKey(K key) {
            MyNode<K, V> curr = head;
            while (curr.key.CompareTo(key) != 0) {
                curr = curr.next;
            }
            return curr;
        }

        public V this[K key] {
            get {
                MyNode<K, V> curr = WalkToKey(key);
                return curr.value;
            }
            set {
                MyNode<K, V> curr = WalkToKey(key);
                curr.value = value;
            }
        }

        public void RemoveKey(K key) {
            MyNode<K, V> curr = WalkToKey(key);
            curr.prev.next = curr.next;
            curr.next.prev = curr.prev;
            count--;
        }



        /*
        /// <summary>
        /// Walks to a specified index in the list
        /// </summary>
        /// <param name="idx"></param>
        /// <returns>The node at the index</returns>
        

        
        */
        /// <summary>
        /// Adds a new node to the back of the list
        /// </summary>
        /// <param name="item"></param>
        public void Add(K key, V value) {


            if (count > 0) {

                MyNode<K, V> curr = new MyNode<K, V>(key, value);

                curr.next = head;

                head.prev = curr;

                head = curr;


            }
            else {

                head = new MyNode<K, V>(key, value);

            }
            count++;
        }

        public V Get(K key, V defaultValue = default) {
            MyNode<K, V> curr = head;
            while (curr != null) {
                if (curr.key.CompareTo(key) == 0) {
                    return curr.value;
                }
                curr = curr.next;
            }
            return defaultValue;
        }

        public bool ContainsKey(K key) {
            MyNode<K, V> curr = head;
            while (curr != null) {
                if (curr.key.CompareTo(key) == 0) {
                    return true;
                }
                curr = curr.next;
            }
            return false;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Integrates IEnumerator functionality
        /// </summary>
        /// <returns>The current reference in the array</returns>
        public IEnumerator<K> GetEnumerator() {
            MyNode<K, V> curr = head;
            while (curr != null) {
                yield return curr.key;
                curr = curr.next;
            }
        }

    }
}
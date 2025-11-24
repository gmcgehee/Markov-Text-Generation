namespace SymbolTable {
    public class TreeSymbolTable<K, V> : IEnumerable<K> where K : IComparable {
        class MyNode<K, V> {

            public MyNode<K, V> L;
            public MyNode<K, V> R;
            public K key;
            public V value;
            public int count;

            public MyNode(K key, V value = default) {
                this.key = key;
                this.value = value;
                this.L = null;
                this.R = null;
                count = 1;
            }
        }

        private MyNode<K, V> root;

        public TreeSymbolTable() {
            root = null;
        }

        public V this[K key] {
            get {
                if (ContainsKey(key)) {
                    MyNode<K, V> curr = GetNode(root, key);
                    return curr.value;
                }
                throw new KeyNotFoundException("Key does not exist.");
            }
            set {
                if (ContainsKey(key)) {
                    MyNode<K, V> curr = GetNode(root, key);
                    curr.value = value;
                }
                else {
                    throw new KeyNotFoundException($"Key \"{key}\" does not exist.");
                }
            }
        }

        public int Count {
            get {
                if (root == null) {
                    return 0;
                }
                else {
                    return root.count;
                }
            }
        }
        /// <summary>
        /// Checks if key exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(K key) {
            if (GetNode(root, key) != null) {
                return true;
            }
            else {
                return false;
            }
        }
        /// <summary>
        /// Returns the node with given key
        /// </summary>
        /// <param name="subroot"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private MyNode<K, V> GetNode(MyNode<K, V> subroot, K key) {
            if (subroot == null) {
                return null;
            }
            if (key.CompareTo(subroot.key) == 0) {
                return subroot;
            }
            else {
                if (key.CompareTo(subroot.key) == -1) {
                    return GetNode(subroot.L, key);
                }
                else if (key.CompareTo(subroot.key) == 1) {
                    return GetNode(subroot.R, key);
                }
            }
            return null;
        }
        /// <summary>
        /// Adds a node
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Add(K key, V value = default) {
            if (key == null) throw new ArgumentNullException($"The key cannot be null");
            root = Add(root, key, value);
        }

        private MyNode<K, V> Add(MyNode<K, V> subroot, K key, V value) {

            if (subroot == null) {
                // New node goes here
                return new MyNode<K, V>(key, value);
            }

            if (key.CompareTo(subroot.key) == -1) {
                subroot.L = Add(subroot.L, key, value);
            }
            else if (key.CompareTo(subroot.key) == 1) {
                subroot.R = Add(subroot.R, key, value);
            }
            else {
                throw new ArgumentException($"A node with the key {key} already exists in the symbol table.");
            }
            subroot.count++;
            return subroot;
        }

        public K Min() {
            return Min(root);
        }

        // K Min()
        /// <summary>
        /// Finds the minimum node
        /// </summary>
        /// <returns>Returns the value of the minimum node</returns>
        /// <exception cref="KeyNotFoundException">Throws an exception if no key is found</exception>
        private K Min(MyNode<K, V> subroot) {

            if (subroot == null) {
                throw new KeyNotFoundException("Tree is empty.");
            }

            while (subroot.L != null) {
                subroot = subroot.L;
            }

            return subroot.key;

        }
        /// <summary>
        /// Finds the maximum node
        /// </summary>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public K Max() {
            MyNode<K, V> subroot = root;
            if (root == null) {
                throw new KeyNotFoundException("Tree is empty.");
            }
            while (subroot.R != null) {
                subroot = subroot.R;
            }
            return subroot.key;
        }

        /// <summary>
        /// Finds the maximum value recursively
        /// </summary>
        /// <returns></returns>
        public K MaxRecursive() {
            return MaxRecursive(root);
        }
        private K MaxRecursive(MyNode<K, V> sub) {
            if (sub == null) {
                throw new KeyNotFoundException("Tree is empty; there is no maximum key.");
            }
            if (sub.R == null) {
                return sub.key;
            }
            return MaxRecursive(sub.R);
        }

        /// <summary>
        /// Finds the predecessor node
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public K Predecessor(K key) {
            MyNode<K, V> subroot = GetNode(root, key);
            if (root == null) {
                throw new KeyNotFoundException("Tree is empty.");
            }

            if (subroot.L != null) {
                subroot = subroot.L;
            }
            while (subroot.R != null) {
                subroot = subroot.R;
            }
            return subroot.key;
        }

        /// <summary>
        /// Finds the successor node
        /// </summary>
        /// <param name="key">The key to search for the successor of</param>
        /// <returns>The successor of the enode</returns>
        /// <exception cref="KeyNotFoundException">Throws an exception if key is not found</exception>
        public K Successor(K key) {
            MyNode<K, V> subroot = GetNode(root, key);
            int relative_order;
            K prev_value = default;

            if (subroot == null) {
                throw new KeyNotFoundException($"The key {key} does not exist in the tree.");
            }

            if (subroot.R == null) {
                throw new KeyNotFoundException($"The key {key} does not have a successor in the tree.");
                /*
               foreach (K keys in GetEnumerator(subroot)) {
                   relative_order = keys.CompareTo(prev_value);
                   if (relative_order == -1 && keys.CompareTo(subroot.key) == 1) {
                       prev_value = keys;
                   }
               }
               return prev_value;
           }
           */
            }
            subroot = subroot.R;

            while (subroot.L != null) {
                subroot = subroot.L;
            }
            return subroot.key;
        }

        private IEnumerable<K> GetEnumerator(MyNode<K, V> subroot) {
            if (subroot != null) {
                foreach (K key in GetEnumerator(subroot.L)) yield return key;
                yield return subroot.key;
                foreach (K key in GetEnumerator(subroot.R)) yield return key;
            }
        }

        public IEnumerator<K> GetEnumerator() {
            foreach (K key in GetEnumerator(root))
                yield return key;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}


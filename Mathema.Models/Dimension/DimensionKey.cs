using Mathema.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Models.Dimension
{
    public class DimensionKey : IDimensionKey
    {
        public Dictionary<string, decimal> Key { get; set; } = new Dictionary<string, decimal>();

        public DimensionKey()
        {
        }

        public DimensionKey(string dim)
        {
            this.Add(dim);
        }

        public void Add(string dim)
        {
            if (this.Key.ContainsKey(dim))
            {
                this.Key[dim] += 1;
            }
            else
            {
                this.Key.Add(dim, 1);
            }
        }

        public void Add(string dim, decimal val)
        {
            if (this.Key.ContainsKey(dim))
            {
                this.Key[dim] += val;
            }
            else
            {
                this.Key.Add(dim, val);
            }
        }

        public void Remove(string dim)
        {
            if (this.Key.ContainsKey(dim))
            {
                this.Key[dim] -= 1;
                if (this.Key[dim] == 0)
                {
                    this.Key.Remove(dim);
                }
            }
            else
            {
                this.Key.Add(dim, -1);
            }
        }

        public void Remove(string dim, decimal val)
        {
            if (this.Key.ContainsKey(dim))
            {
                this.Key[dim] -= val;
                if (this.Key[dim] == 0)
                {
                    this.Key.Remove(dim);
                }
            }
            else
            {
                this.Key.Add(dim, -val);
            }
        }

        public void Multiply(string dim, decimal val)
        {
            if (this.Key.ContainsKey(dim))
            {
                this.Key[dim] *= val;
            }
            else
            {
                throw new ArgumentException($"Couldn't find that key '{dim}' in expression dimensions");
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var ordered = this.Key.OrderBy(kv => kv.Key);
            foreach (var item in ordered)
            {
                if (item.Value > 0)
                {
                    sb.Append(string.Join(" * ", Enumerable.Repeat(item.Key, (int)item.Value)));
                }
                else
                {
                    sb.Append(string.Join(" / ", Enumerable.Repeat(item.Key, (int)item.Value)));
                }
            }

            return sb.ToString();
        }

        public static bool operator ==(DimensionKey a, IDimensionKey b)
        {
            if (a.Key.Count != b.Key.Count)
            {
                return false;
            }

            for (int i = 0; i < a.Key.Count; i++)
            {
                var k = a.Key.ElementAt(i).Key;
                if (!b.Key.ContainsKey(k))
                {
                    return false;
                }

                if (a.Key[k] != b.Key[k])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(DimensionKey a, IDimensionKey b)
        {
            return !(a == b);
        }

    }
}

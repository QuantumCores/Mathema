using Mathema.Enums.DimensionKeys;
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
            this.Add(dim, 1);
        }

        public void Add(string dim, decimal val)
        {
            if (this.Key.ContainsKey(dim))
            {
                this.Key[dim] += val;
                if (this.Key[dim] == 0)
                {
                    this.Key.Remove(dim);
                }
            }
            else
            {
                this.Key.Add(dim, val);
            }
        }

        public void Add(IDimensionKey dimensionKey)
        {
            foreach (var dim in dimensionKey.Key)
            {
                this.Add(dim.Key, dim.Value);
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

        public static bool Compare(IDimensionKey a, IDimensionKey b)
        {
            //int an = 0;
            //int bn = 0;
            //if (a.Key.ContainsKey(Dimensions.Number) && !b.Key.ContainsKey(Dimensions.Number))
            //{
            //    bn = 1;
            //}
            //else if (b.Key.ContainsKey(Dimensions.Number) && !a.Key.ContainsKey(Dimensions.Number))
            //{
            //    an = 1;
            //}

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

        public IDimensionKey Clone()
        {
            var res = new DimensionKey();

            foreach (var kv in this.Key)
            {
                res.Key.Add(string.Copy(kv.Key), kv.Value);
            }

            return res;
        }

        public override string ToString()
        {
            var sb = new List<string>();

            var ordered = this.Key.OrderBy(kv => kv.Key);
            foreach (var item in ordered)
            {
                if (item.Value > 0)
                {
                    if (sb.Count > 0)
                    {
                        sb.Add(" * ");
                    }
                    sb.Add(string.Join(" * ", Enumerable.Repeat(item.Key, (int)item.Value)));
                }
                else
                {
                    if (sb.Count > 0)
                    {
                        sb.Add(" / ");
                    }
                    var ump = Enumerable.Repeat(item.Key, -(int)item.Value);
                    var tmp = string.Join(" / ", ump);
                    sb.Add(tmp);
                }
            }

            return string.Join("", sb);
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is DimensionKey key)
            {
                return Compare(this, key);
            }

            return false;
        }

        public static bool operator ==(DimensionKey a, IDimensionKey b)
        {
            return Compare(a, b);
        }

        public static bool operator !=(DimensionKey a, IDimensionKey b)
        {
            return !(a == b);
        }
    }
}

using System;

namespace OOP_Protasiuk
{
    public class BitMask
    {
        private int _maskValue;

        public int MaskValue
        {
            get { return _maskValue; }
            set
            {
                _maskValue = value;
            }
        }

        public static BitMask AllSet
        {
            get { return new BitMask(-1); }
        }

        public bool this[int bitIndex]
        {
            get
            {
                if (bitIndex < 0 || bitIndex >= 32)
                    throw new IndexOutOfRangeException("Індекс біта має бути від 0 до 31.");

                return (_maskValue & (1 << bitIndex)) != 0;
            }
            set
            {
                if (bitIndex < 0 || bitIndex >= 32)
                    throw new IndexOutOfRangeException("Індекс біта має бути від 0 до 31.");

                if (value)
                    _maskValue |= (1 << bitIndex);
                else
                    _maskValue &= ~(1 << bitIndex);
            }
        }

        public BitMask(int value)
        {
            _maskValue = value;
        }

        public static BitMask operator &(BitMask a, BitMask b)
        {
            return new BitMask(a._maskValue & b._maskValue);
        }

        public static BitMask operator |(BitMask a, BitMask b)
        {
            return new BitMask(a._maskValue | b._maskValue);
        }

        public static bool operator ==(BitMask a, BitMask b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null || b is null)
                return false;

            return a._maskValue == b._maskValue;
        }

        public static bool operator !=(BitMask a, BitMask b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return Convert.ToString(_maskValue, 2).PadLeft(32, '0');
        }

        public override bool Equals(object? obj)
        {
            if (obj is BitMask other)
                return _maskValue == other._maskValue;

            return false;
        }

        public override int GetHashCode()
        {
            return _maskValue.GetHashCode();
        }
    }

    class Program
    {
        static void Main()
        {
            BitMask mask1 = new BitMask(10);
            BitMask mask2 = new BitMask(12);

            Console.WriteLine("=== BitMask ===");

            Console.WriteLine($"Mask 1: {mask1}");
            Console.WriteLine($"Mask 2: {mask2}");

            Console.WriteLine("\n=== Властивість MaskValue ===");
            Console.WriteLine($"Значення mask1: {mask1.MaskValue}");

            mask1.MaskValue = 15;
            Console.WriteLine($"Нове значення mask1: {mask1.MaskValue}");

            Console.WriteLine("\n=== Індексатор ===");

            Console.WriteLine($"Біт 0: {mask1[0]}");
            Console.WriteLine($"Біт 1: {mask1[1]}");
            Console.WriteLine($"Біт 2: {mask1[2]}");
            Console.WriteLine($"Біт 3: {mask1[3]}");

            mask1[4] = true;
            Console.WriteLine($"Після встановлення біта 4: {mask1.MaskValue}");

            mask1[1] = false;
            Console.WriteLine($"Після вимкнення біта 1: {mask1.MaskValue}");

            Console.WriteLine("\n=== Статичний член AllSet ===");
            BitMask all = BitMask.AllSet;
            Console.WriteLine($"AllSet: {all}");

            Console.WriteLine("\n=== Оператор & ===");
            BitMask andResult = mask1 & mask2;
            Console.WriteLine($"{mask1.MaskValue} & {mask2.MaskValue} = {andResult.MaskValue}");

            Console.WriteLine("\n=== Оператор | ===");
            BitMask orResult = mask1 | mask2;
            Console.WriteLine($"{mask1.MaskValue} | {mask2.MaskValue} = {orResult.MaskValue}");

            Console.WriteLine("\n=== == та != ===");
            BitMask mask3 = new BitMask(mask1.MaskValue);

            Console.WriteLine($"mask1 == mask3: {mask1 == mask3}");
            Console.WriteLine($"mask1 != mask2: {mask1 != mask2}");

            Console.WriteLine("\n=== Equals() та GetHashCode() ===");
            Console.WriteLine($"mask1.Equals(mask3): {mask1.Equals(mask3)}");
            Console.WriteLine($"HashCode mask1: {mask1.GetHashCode()}");
        }
    }
}
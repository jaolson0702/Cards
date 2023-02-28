using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class Counter : IComparable<Counter>, IComparable<double>
    {
        private double? lowerLimit, upperLimit, previous = null;
        private double resetValue, factor, value;
        private bool isActive = true;
        public bool LimitSetExceptionEnabled = true, InactiveExceptionEnabled = true;
        public Action OnResetValueChanged = null, OnFactorChanged = null, OnValueChanged = null, OnLowerLimitChanged = null, OnUpperLimitChanged = null;
        public Action OnLowerLimitExceeded = () => throw new ArgumentOutOfRangeException("NextLower", "The referenced value is less than the current lower limit."),
            OnUpperLimitExceeded = () => throw new ArgumentOutOfRangeException("NextUpper", "The referenced value is greater than the current upper limit.");
        public Predicate<double> LowerLimitConditionE = arg => true, UpperLimitConditionE = arg => true, ResetValueConditionC = arg => true,
            FactorConditionC = arg => true, ValueConditionC = arg => true, LowerLimitConditionC = arg => true, UpperLimitConditionC = arg => true;
        public bool IsActive
        {
            get => isActive && Includes(ResetValue) && Includes(Value);
            set => isActive = value;
        }
        public double? Previous => previous;
        public double ResetValue
        {
            get => resetValue;
            set
            {
                resetValue = value;
                if (ResetValueConditionC(Value)) OnResetValueChanged?.Invoke();
            }
        }
        public double Factor
        {
            get => factor;
            set
            {
                factor = value;
                if (FactorConditionC(Value)) OnFactorChanged?.Invoke();
            }
        }
        public double Value
        {
            get => value;
            set
            {
                previous = this.value;
                this.value = value;
                if (ValueConditionC(Value)) OnValueChanged?.Invoke();
            }
        }
        public double? UpperLimit
        {
            get => upperLimit;
            set
            {
                if (value is not null && LowerLimit is not null && value < (double)LowerLimit)
                {
                    if (LimitSetExceptionEnabled)
                        throw new ArgumentOutOfRangeException("UpperLimit", "The specified upper limit is less than the current lower limit.");
                }
                else
                {
                    upperLimit = value;
                    OnUpperLimitChanged?.Invoke();
                }
            }
        }
        public double? LowerLimit
        {
            get => lowerLimit;
            set
            {
                if (lowerLimit is not null && UpperLimit is not null && lowerLimit > (double)UpperLimit)
                {
                    if (LimitSetExceptionEnabled)
                        throw new ArgumentOutOfRangeException("LowerLimit", "The specified lower limit is greater than the current upper limit.");
                }
                else
                {
                    lowerLimit = value;
                    OnLowerLimitChanged?.Invoke();
                }
            }
        }
        public Counter(double? lowerLimit, double? upperLimit, double factor, double value) =>
            (LowerLimit, UpperLimit, ResetValue, Factor, this.value) = (lowerLimit, upperLimit, value, factor, value);
        public Counter(double factor = 1, double value = 0) : this(null, null, factor, value) { }
        public double NextUpper
        {
            get
            {
                if (!Includes(Value + Factor))
                {
                    OnLimitExceeded(true);
                    return Value;
                }
                return Value + Factor;
            }
        }
        public double NextLower
        {
            get
            {
                if (!Includes(Value - Factor))
                {
                    OnLimitExceeded(false);
                    return Value;
                }
                return Value - Factor;
            }
        }
        public double? OneOver
        {
            get
            {
                if (UpperLimit is null) return null;
                Counter result = new(LowerLimit, UpperLimit, Factor, Value);
                while (result.LowerLimit is not null && result <= (double)result.LowerLimit)
                    (result.ResetValue, result.Value) = (result.ResetValue + Factor, result.Value + Factor);
                while ((result.Value - Factor) >= (double)result.UpperLimit) (result.ResetValue, result.Value) = (result.ResetValue - Factor, result.Value - Factor);
                if (!result.IsActive) throw new NarrowRegionException("The counter's region is too narrow for the given factor with the given value.");
                while (result.Includes(result.Value + result.Factor)) result.Increment();
                return result.Value + result.Factor;
            }
        }
        public double? OneUnder
        {
            get
            {
                if (LowerLimit is null) return null;
                Counter result = new(LowerLimit, UpperLimit, Factor, Value);
                while (result.UpperLimit is not null && result >= (double)result.UpperLimit)
                    (result.ResetValue, result.Value) = (result.ResetValue - Factor, result.Value - Factor);
                while (result.Value + Factor <= (double)result.LowerLimit) (result.ResetValue, result.Value) = (result.ResetValue + Factor, result.Value + Factor);
                if (!result.IsActive) throw new NarrowRegionException("The counter's region is too narrow for the given factor with the given value.");
                while (result.Includes(result.Value - result.Factor)) result.Decrement();
                return result.Value - result.Factor;
            }
        }
        public double? UpperBorder => OneOver - Factor;
        public double? LowerBorder => OneUnder + Factor;
        public void Increment() => Value = NextUpper;
        public void Decrement() => Value = NextLower;
        public void IncrementNTimes(ushort n) { for (int a = 0; a < n; a++) Increment(); }
        public void DecrementNTimes(ushort n) { for (int a = 0; a < n; a++) Decrement(); }
        public void Reset() => Value = ResetValue;
        public bool Includes(double given) => !((LowerLimit is not null && given <= (double)LowerLimit) || (UpperLimit is not null && given >= (double)UpperLimit));
        public void EnsureActivity()
        {
            if (!IsActive && InactiveExceptionEnabled)
                throw new InactiveException("The counter is currently inactive as a result of the reset value, the current value, or both not being in the acceptable region specified by the lower and upper limits.");
        }
        private void OnLimitExceeded(bool isUpperLimit)
        {
            if (isUpperLimit && UpperLimitConditionE(Value)) OnUpperLimitExceeded?.Invoke();
            if (!isUpperLimit) OnLowerLimitExceeded?.Invoke();
        }
        public int CompareTo(Counter other) => Value.CompareTo(other.Value);
        public int CompareTo(double other) => Value.CompareTo(other);
        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString()
        {
            string result = $"Counter ({(LowerLimit is not null ? $"Lower Limit: {LowerLimit}, " : "")}{(UpperLimit is not null ? $"Upper Limit: {UpperLimit}, " : "")}";
            result += $"Factor: {Factor}, Value: {Value}, Reset Value: {ResetValue}, Status: {(IsActive ? "Active" : "Inactive")})";
            return result;
        }
        public static bool operator ==(Counter a, Counter b) => a.CompareTo(b) == 0;
        public static bool operator !=(Counter a, Counter b) => !(a == b);
        public static bool operator >(Counter a, Counter b) => a.CompareTo(b) > 0;
        public static bool operator <(Counter a, Counter b) => a.CompareTo(b) < 0;
        public static bool operator >=(Counter a, Counter b) => !(a < b);
        public static bool operator <=(Counter a, Counter b) => !(a > b);
        public static bool operator ==(Counter a, double b) => a.CompareTo(b) == 0;
        public static bool operator !=(Counter a, double b) => !(a == b);
        public static bool operator >(Counter a, double b) => a.CompareTo(b) > 0;
        public static bool operator <(Counter a, double b) => a.CompareTo(b) < 0;
        public static bool operator >=(Counter a, double b) => !(a < b);
        public static bool operator <=(Counter a, double b) => !(a > b);
    }
}
using System;

namespace ERP.Utils
{
    public class Condition
    {
        public String field { get; set; }

        public Operator op { get; set; }

        public ValType valueType { get; set; }

        public String value { get; set; }
    }

    public enum Operator
    {
        EqualTo,
        GreaterThan,
        //GreaterThanOrEqualTo,
        //LessThan,
        //LessThanOrEqualTo
    }

    public enum ValType
    {
        Int,
        //Float,
        String,
        DateTime
    }
}

using UnityEngine;

namespace Core.attribute
{

    public class ShowIfAttribute : PropertyAttribute
    {
        public string ConditionFieldName;

        public ShowIfAttribute(string conditionFieldName)
        {
            this.ConditionFieldName = conditionFieldName;
        }
    }
}
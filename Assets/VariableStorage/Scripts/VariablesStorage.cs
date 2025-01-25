using System.Collections.Generic;
using UnityEngine;

namespace CoreTeamGamesSDK.Other.VariableStorage
{
    ///<summary>
    /// The in-game storage of variables
    ///</summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("CoreTeam Games SDK/Other/Variables Storage")]
    public class VariablesStorage : MonoBehaviour
    {
        #region Variables
        [SerializeField] private List<Variable> _variables;
        #endregion

        #region Properties
        public int Count => _variables != null ? _variables.Count : 0;
        #endregion

        #region Methods
        public Variable GetVariable(string variableName)
        {
            if (variableName == "")
            {
                Debug.LogError($"Variable name is null!");
                return null;
            }

            foreach (var variable in _variables)
            {
                if (variable.VariableName == variableName)
                    return variable;
            }
            // Return null if variable not found
            Debug.LogError($"Variable with name {'"'}{variableName}{'"'} doesn{"'"}t exist");
            return null;
        }

        public bool VariableExist(string variableName)
        {
            if (variableName == "")
            {
                Debug.LogError($"Variable name is null!");
                return false;
            }

            foreach (var variable in _variables)
            {
                if (variable.VariableName == variableName)
                    return true;
            }
            // Return false if variable not found
            return false;
        }

        public void RemoveVariable(string variableName)
        {
            if (variableName == "")
            {
                Debug.LogError($"Variable name is null!");
                return;
            }

            if (!VariableExist(variableName))
            {
                Debug.LogError($"Variable with name {'"'}{variableName}{'"'} doesn{"'"}t exist");
                return;
            }

            _variables.Remove(GetVariable(variableName));
        }

        public void AddVariable(Variable variable)
        {
            if (variable == null)
            {
                Debug.LogError($"Variable is null!");
                return;
            }

            if (variable.VariableName == "")
            {
                Debug.LogError($"Variable name is null!");
                return;
            }

            if (VariableExist(variable.VariableName))
            {
                Debug.LogWarning($"Variable with name {'"'}{variable.VariableName}{'"'} already exist");
                return;
            }
            else
                _variables.Add(variable);
        }
        #endregion
    }
}
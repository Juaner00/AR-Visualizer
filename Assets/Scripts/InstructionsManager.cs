using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionsRoot;
    [SerializeField] private GameObject[] instructions;
    
    private int _currentInstruction = 0;
    
    private void Awake()
    {
        instructionsRoot.SetActive(true);
        foreach (var instruction in instructions)
        {
            instruction.SetActive(false);
        }
        instructions[0].SetActive(true);
    }

    public void NextInstruction()
    {
        if (_currentInstruction == instructions.Length - 1)
        {
            OnPlay();
            return;
        }
        
        instructions[_currentInstruction].SetActive(false);
        instructions[++_currentInstruction].SetActive(true);
    }
    
    private void OnPlay()
    {
        instructionsRoot.SetActive(false);
    }
}

using UnityEngine;

public interface ICharacterInput
{
    Vector3 GetLookTarget();
    
    Vector2 GetNormalizedMovement();
    
    bool IsAttacking();
    
    bool IsBlocking();
}
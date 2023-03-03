using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AvatarCustomization : MonoBehaviour
{
    SkinnedMeshRenderer skin;
    public Mesh ModelToUse;
    // Start is called before the first frame update
    void Start()
    {
        skin = GetComponent<SkinnedMeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.Z].isPressed) {
            ChangeMesh();
        }
    }

    void ChangeMesh() {
        //skin.sharedMesh = Resources.Load<Mesh>("Assets/Low poly 3D game character_1/Animations/Character_1@Forward Flip.fbx");
        //GetComponent<Renderer>().material ;
        skin.sharedMesh = ModelToUse;
    }

    void toSit() {

      
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ArrowController : CreatureController
{
    protected override void Init()
    {
        switch (_lastDir)
        {
            case MoveDir.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case MoveDir.Down:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case MoveDir.Left:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case MoveDir.Right:
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
        }

        base.Init();
    }

    protected override void UpdateAnimation()
    {
        
    }

    protected override void UpdateIdle()
    {
        if (_dir != MoveDir.None)
        {
            Vector3Int dest = CellPos;

            switch (_dir)
            {
                case MoveDir.Up:
                    dest += Vector3Int.up;
                    break;
                case MoveDir.Down:
                    dest += Vector3Int.down;
                    break;
                case MoveDir.Left:
                    dest += Vector3Int.left;
                    break;
                case MoveDir.Right:
                    dest += Vector3Int.right;
                    break;
            }

            State = CreatureState.Moving;

            if (Managers.Map.CanGo(dest))
            {
                GameObject go = Managers.Object.Find(dest);
                if (go == null)
                {
                    CellPos = dest;
                }
                else
                {
                    CreatureController cc = go.GetComponent<CreatureController>();
                    if (cc != null)
                    {
                        cc.OnDamaged();
                    }

                    Managers.Resource.Destroy(gameObject);
                }
            }
            else
            {
                Managers.Resource.Destroy(gameObject);
            }
        }
    }
}

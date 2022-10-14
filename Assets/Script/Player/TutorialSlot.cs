using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class TutorialSlot : MonoBehaviour
{
    [SpineAttachment]
    public string Halmet;
    [SpineAttachment]
    public string neckScarf;
    [SpineAttachment]
    public string lftarmr;
    [SpineAttachment]
    public string rytArmr;
    [SpineAttachment]
    public string bow;
    [SpineAttachment]
    public string sword;
    [SpineAttachment]
    public string quiver;

    // Start is called before the first frame update
    void Start()
    {
        SkeletonSlot(Halmet);
        SkeletonSlot(neckScarf);
        SkeletonSlot(lftarmr);
        SkeletonSlot(rytArmr);
        SkeletonSlot(bow);
        SkeletonSlot(sword);
        SkeletonSlot(quiver);
    }

    void SkeletonSlot(string _spine)
    {
        if (_spine != null)
        {
            Spine.Slot slot = GetComponent<SkeletonAnimation>().Skeleton.FindSlot(_spine);
            if (slot != null)
            {
                slot.Attachment = null;
            }
        }
    }
}

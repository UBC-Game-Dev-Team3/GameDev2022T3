using System;
using UnityEngine;
using UnityEngine.UI;

namespace SymbolBook
{
    /// <summary>
    ///     Represents a symbol for the symbol book
    /// </summary>
    [CreateAssetMenu(fileName = "New Symbol", menuName = "SymbolBook/Symbol")]
    public class Symbol : ScriptableObject
    {
        public static readonly string SymbolDefaultName = "???";
        [Tooltip("Whether this is a Word or a Normal Symbol")]
        public bool isWord = false;
        [Tooltip("Name of Symbol (hidden to user)")]
        public string symbolName = "New Symbol";
        /// <summary>
        /// Symbol name given by the user. Is likely incorrect.
        /// </summary>
        [NonSerialized]
        public string PlayerSymbolName = SymbolDefaultName;
        [NonSerialized]
        public bool HasPlayerModified = false;
        [Tooltip("Take a wild guess")]
        public Sprite image;
        [Tooltip("Highlighted Image")]
        public Sprite highlightImage;
        /// <summary>
        /// Player notes written by the user. Is also likely incorrect.
        /// </summary>
        [NonSerialized]
        public string PlayerNotes = "";
        [Tooltip("Whether this starts seen.")]
        public bool startSeen; 
        /// <summary>
        /// Whether the player saw this.
        /// </summary>
        [NonSerialized] public bool SeenByPlayer;
        [Tooltip("List of Symbols this Makes Up")]
        public Symbol[] contents;

        private void Awake()
        {
            PlayerSymbolName = SymbolDefaultName;
            HasPlayerModified = false;
            SeenByPlayer = false;
            PlayerNotes = "";
        }

        public void Render(GameObject parent, bool raycastTarget = true, bool highlight = false, bool shouldBeBlack = false)
        {
            Image sprite = parent.GetComponent<Image>();
            int childCount = parent.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                if (parent.transform.GetChild(i).gameObject.name != "Highlight") continue;
                parent.transform.GetChild(i).SetAsFirstSibling();
                childCount--;
                break;
            }
            Symbol[] scrollSymbols = contents;
            int desiredScrollChild = isWord ? scrollSymbols.Length : 0;
            for (int i = childCount-1; i >= desiredScrollChild; i--)
            {
                Destroy(parent.transform.GetChild(i).gameObject);
            }

            if (sprite != null) sprite.enabled = !isWord;

            HorizontalLayoutGroup layoutGroup = parent.GetComponent<HorizontalLayoutGroup>();
            ContentSizeFitter fitter = parent.GetComponent<ContentSizeFitter>();
            if (layoutGroup != null) layoutGroup.enabled = isWord;
            if (fitter != null) fitter.enabled = isWord;

            if (isWord)
            {
                for (int i = childCount; i < desiredScrollChild; i++)
                {
                    GameObject go = new("Child1", typeof(Image));
                    go.transform.SetParent(parent.transform, false);
                }

                for (int i = 0; i < desiredScrollChild; i++)
                {
                    Transform child = sprite.transform.GetChild(i);
                    Image imageChild = child.GetComponent<Image>();
                    if (shouldBeBlack) imageChild.color = Color.black;
                    
                    Sprite spriteI = highlight
                        ? contents[i].highlightImage == null ? contents[i].image : contents[i].highlightImage
                        : contents[i].image;
                    imageChild.sprite = spriteI;
                    imageChild.raycastTarget = raycastTarget;
                    imageChild.preserveAspect = true;
                }
            } else if (sprite != null)
            {
                sprite.preserveAspect = true;
                sprite.sprite = highlight ? highlightImage : image;
            }
        }
    }
}
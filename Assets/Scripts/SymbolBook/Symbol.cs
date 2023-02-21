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
        [Tooltip("Take a wild guess")]
        public Sprite image;
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

        public void Render(GameObject parent, int spriteSize = 100, bool raycastTarget = true)
        {
            Image sprite = parent.GetComponent<Image>();
            int childCount = parent.transform.childCount;
            Symbol[] scrollSymbols = contents;
            int desiredScrollChild = isWord ? scrollSymbols.Length : 0;
            for (int i = childCount-1; i >= desiredScrollChild; i--)
            {
                Destroy(parent.transform.GetChild(i).gameObject);
            }

            if (sprite != null) sprite.enabled = !isWord;

            if (isWord)
            {
                for (int i = childCount; i < desiredScrollChild; i++)
                {
                    GameObject go = new("Child1", typeof(Image));
                    go.transform.SetParent(parent.transform, false);
                }

                float scaleFactor = 1;
                float width = 0;
                for (int i = 0; i < desiredScrollChild; i++)
                {
                    scaleFactor = Math.Min(scaleFactor, spriteSize / contents[i].image.rect.height);
                    width += contents[i].image.rect.width;
                }

                float xPos = -(width / 2) * scaleFactor;
                for (int i = 0; i < desiredScrollChild; i++)
                {
                    Transform child = sprite.transform.GetChild(i);
                    Image imageChild = child.GetComponent<Image>();
                    imageChild.sprite = contents[i].image;
                    RectTransform rectTransform = child.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(imageChild.preferredWidth*scaleFactor, spriteSize);
                    imageChild.raycastTarget = raycastTarget;
                    xPos += imageChild.preferredWidth * scaleFactor / 2;
                    Vector3 position = rectTransform.localPosition;
                    float prev = position.x;
                    position += new Vector3(xPos - prev,0,0);
                    rectTransform.localPosition = position;
                    xPos += imageChild.preferredWidth* scaleFactor/2;
                }
            } else if (sprite != null)
            {
                sprite.preserveAspect = true;
                sprite.sprite = image;
            }
        }
    }
}
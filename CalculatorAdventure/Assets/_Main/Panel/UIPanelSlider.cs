using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // 務必引用 DOTween 命名空間

public class UIPanelSlider : MonoBehaviour
{
    [Header("UI 元件")]
    [SerializeField] private RectTransform panelRect; // 要移動的 UI 面板
    [SerializeField] private Button toggleButton;    // 觸發按鈕

    [Header("動畫設定")]
    [SerializeField] private Vector2 outPosition;    // 畫面外的座標
    [SerializeField] private Vector2 inPosition;     // 畫面內的座標
    [SerializeField] private float duration = 0.5f;  // 動畫持續時間
    [SerializeField] private Ease showEase = Ease.OutBack; // 彈出效果
    [SerializeField] private Ease hideEase = Ease.InQuad;  // 縮回效果

    private bool isOpen = false; // 目前狀態

    private void Start()
    {
        // 初始化：確保面板一開始在畫面外
        if (panelRect != null)
        {
            panelRect.anchoredPosition = outPosition;
        }

        // 綁定按鈕點擊事件
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(TogglePanel);
        }
    }

    /// <summary>
    /// 切換面板狀態的主邏輯
    /// </summary>
    public void TogglePanel()
    {
        // 停止該物件上正在進行的 DOTween 動畫，避免快速連續點擊造成的抖動
        panelRect.DOKill();

        if (isOpen)
        {
            // 往回縮（移至畫面外）
            panelRect.DOAnchorPos(outPosition, duration)
                .SetEase(hideEase)
                .SetUpdate(true); // 設定為 true 可讓 Time.timeScale = 0 時也能執行
        }
        else
        {
            // 拖過來（移至畫面內）
            panelRect.DOAnchorPos(inPosition, duration)
                .SetEase(showEase)
                .SetUpdate(true);
        }

        isOpen = !isOpen; // 反轉狀態
    }

    // 建議在物件銷毀時清理 Tweener，防止記憶體洩漏
    private void OnDestroy()
    {
        panelRect.DOKill();
    }
}
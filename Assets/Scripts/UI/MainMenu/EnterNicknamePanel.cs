using TMPro;
using UnityEngine;

public class EnterNicknamePanel : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    public void TryToConfirmNickname(TMP_InputField inputField)
    {
        string nickname = inputField.text.Trim();
        nickname = System.Text.RegularExpressions.Regex.Replace(nickname, @"\s+", " ");
        inputField.text = nickname;

        if (!string.IsNullOrEmpty(nickname))
            playerData.nickname = nickname;
    }
}

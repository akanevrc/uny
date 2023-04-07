using UnityEngine;
using UnityEngine.UIElements;

namespace akanevrc.Uny
{
    public class UnyUI : MonoBehaviour
    {
        private TextField CodePane { get; set; }
        private Button RunButton { get; set; }

        private void Start()
        {
            var doc = GetComponent<UIDocument>();
            var root = doc.rootVisualElement;
            CodePane = root.Q<TextField>("codePane");
            RunButton = root.Q<Button>("runButton");

            RunButton.clicked += RunButtonClicked;
        }

        private void RunButtonClicked()
        {
            UnyCompiler.Compile(CodePane.text);
            new UnyRunner().Run();
        }
    }
}

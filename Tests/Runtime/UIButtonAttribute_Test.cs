using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class UIButtonAttribute_Test
    {
        public class HasUIButtonTester : MonoBehaviour
        {
            public enum EButtonName
            {
                Button_A,
                Button_B,
            }

            public int iButton_A_ClickCount { get; private set; }

            public Button pButtonClicked { get; private set; }

            public void Reset()
            {
                iButton_A_ClickCount = 0;
                pButtonClicked = null;
            }

            [UIButtonCall(EButtonName.Button_A)]
            public void ��ư_A_��_1()
            {
                iButton_A_ClickCount++;
            }

            [UIButtonCall(nameof(EButtonName.Button_A))]
            public void ��ư_A_��_2()
            {
                iButton_A_ClickCount++;
            }

            [UIButtonCall(EButtonName.Button_B)]
            public void ��ư_B_��(Button pButtonInstance)
            {
                pButtonClicked = pButtonInstance;
            }

        }

        // ====================================================================================== //

        [Test]
        public void �����̸���_���ڰ�����_��ư��_��ɵ����׽�Ʈ()
        {
            // Arrange (������ ����)
            GameObject pObjectTester = new GameObject(nameof(�����̸���_���ڰ�����_��ư��_��ɵ����׽�Ʈ));
            Button pButtonA = Create_ChildButton(pObjectTester, HasUIButtonTester.EButtonName.Button_A.ToString());
            Create_ChildButton(pObjectTester, HasUIButtonTester.EButtonName.Button_B.ToString());

            // ���� Ŭ�� �̺�Ʈ ������
            PointerEventData pPointerEventData = new PointerEventData(null);

            HasUIButtonTester pTester = pObjectTester.AddComponent<HasUIButtonTester>();
            pTester.Reset();
            Assert.AreEqual(pTester.iButton_A_ClickCount, 0);



            // Act (����)
            UIButtonAttributeSetter.DoUpdate_UIButtonAttribute(pTester);
            // ��ũ��Ʈ�� �������� ��ư A Ŭ��
            pButtonA.OnPointerClick(pPointerEventData);



            // Assert(�´��� üũ)
            Assert.AreEqual(pTester.iButton_A_ClickCount, 2);
        }


        [Test]
        public void ���ڰ�1���ִ�_��ư��_��ɵ����׽�Ʈ()
        {
            // Arrange (������ ����)
            GameObject pObjectTester = new GameObject(nameof(���ڰ�1���ִ�_��ư��_��ɵ����׽�Ʈ));
            Button pButtonB = Create_ChildButton(pObjectTester, HasUIButtonTester.EButtonName.Button_B.ToString());
            Create_ChildButton(pObjectTester, HasUIButtonTester.EButtonName.Button_A.ToString());

            // ���� Ŭ�� �̺�Ʈ ������
            PointerEventData pPointerEventData = new PointerEventData(null);

            HasUIButtonTester pTester = pObjectTester.AddComponent<HasUIButtonTester>();
            pTester.Reset();
            Assert.IsNull(pTester.pButtonClicked);



            // Act (����)
            UIButtonAttributeSetter.DoUpdate_UIButtonAttribute(pTester);
            // ��ũ��Ʈ�� �������� ��ư A Ŭ��
            pButtonB.OnPointerClick(pPointerEventData);



            // Assert(�´��� üũ)
            Assert.AreEqual(pTester.pButtonClicked, pButtonB);
        }

        // ====================================================================================== //

        private static Button Create_ChildButton(GameObject pObjectTester, string strButtonName)
        {
            Button pSomethingButton = new GameObject(strButtonName).AddComponent<Button>();
            pSomethingButton.transform.SetParent((pObjectTester.transform));

            return pSomethingButton;
        }
    }
}

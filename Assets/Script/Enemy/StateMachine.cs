using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�g�}�V��
/// </summary>
public class StateMachine<TOwner>
{
    ///<summary>�X�e�[�g��\���N���X </summary>
    public abstract class State
    {
        /// <summary>���̃X�e�[�g���Ǘ����Ă���X�e�[�g�}�V�� </summary>
        protected StateMachine<TOwner> StateMachine => stateMachine;
        internal StateMachine<TOwner> stateMachine;
        /// <summary>�J�ڂ̈ꗗ </summary>
        internal Dictionary<int, State> transitions = new Dictionary<int, State>();
        /// <summary>���̃X�e�[�g�̃I�[�i�[</summary>
        protected TOwner Owner => stateMachine.Owner;

        /// <summary>�X�e�[�g�J�n</summary>
        internal void Enter(State prevState)
        {
            OnEnter(prevState);
        }
        /// <summary> �X�e�[�g���J�n�������ɌĂ΂�� </summary>
        protected virtual void OnEnter(State prevState) { }

        /// <summary> �X�e�[�g�X�V </summary>
        internal void Update()
        {
            OnUpdate();
        }
        /// <summary> ���t���[���Ă΂��</summary>
        protected virtual void OnUpdate() { }

        /// <summary> �X�e�[�g�I�� </summary>
        internal void Exit(State nextState)
        {
            OnExit(nextState);
        }
        /// <summary> �X�e�[�g���I���������ɌĂ΂��</summary>
        protected virtual void OnExit(State nextState) { }
    }


    ///<summary>���̃X�e�[�g�}�V���̃I�[�i�[ </summary>
    public TOwner Owner { get; }

    ///<summary>�X�e�[�g�}�V���̃R���X�g���N�^ </summary>
    ///<param name="owner">�X�e�[�g�}�V���̃I�[�i�[</param>
    public StateMachine(TOwner owner)
    {
        Owner = owner;
    }

}

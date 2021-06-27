// recoil example

import { atom, selector } from "recoil"

export const doFilteringState = atom<boolean>({
    key:"doFilteringState",
    default: false,
})

export const counterState = atom<number>({
    key: "counterState",
    default: 0,
});

export const newCounterValueState = selector<number>({
    key: 'newCounterValueState',
    get: ({get}) => {
        return (Math.pow(get(counterState), 2) + 40) / 2;
    },
});

// 회원가입 시 각종 입력들을 atom 단위로 구현
// atom에 대해 selector 구현해서 validation 손쉽게 구현 가능

export const nameState = atom<string>({
    key: 'nameState',
    default: ''
});

export const validateFormValues = selector<boolean>({
    key: "validationFormValues",
    get: ({ get }) => {
        const name = get(nameState)
        /**
         * Validation logic
         *  */
        return false // validation의 결과물
    }
})
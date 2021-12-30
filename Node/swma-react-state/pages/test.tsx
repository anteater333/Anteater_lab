import React from 'react';

import {
    Button,
    Checkbox,
    Editable,
    EditableInput,
    EditablePreview,
    Spinner
} from '@chakra-ui/react';
import useTodoList from '../hooks/useTodoList';
import produce from 'immer';
import _ from 'lodash';
import {Record} from '../interfaces'
import { counterState, doFilteringState, newCounterValueState } from "../store"
import { useRecoilState, useRecoilValue, useSetRecoilState } from 'recoil';

const Test = () => {
    // custom hook
    const { todoList, addNewTodo, updateTodo, deleteTodo, isLoading } =
        useTodoList();
    const [counter, setCounter] = useRecoilState(counterState)
    const newCounterValue = useRecoilValue(newCounterValueState)
    const [doFiltering, setDoFiltering] = useRecoilState(doFilteringState)

    return (
        <div style={{ padding: 10 }}>
            {isLoading && <Spinner/>}
            <div>
                <Checkbox defaultChecked={doFiltering} onChange={(e) => {
                    const checked = e.target.checked;
                    setDoFiltering(checked);
                }} /> 안한 일만 보기{" "}
            </div>
            {/* <div>
                <div>counter: {counter}, newCounter: {newCounterValue}</div>
                <button
                    onClick={()=>{setCounter(counter + 1);}}>+</button>
            </div> */}
            {todoList.map((todo) => {
                const debouncingInput = _.debounce((record: Record, nextValue: string) => {
                    const newTodo = produce(todo, (nextTodo) => {
                        nextTodo.fields.Name = nextValue;
                    });
                    updateTodo(newTodo);
                }, 1500)
                return (
                    <div
                        key={
                            todo.id + '/' +
                            todo.fields.Name
                        }
                        style={{ display: 'flex', marginBottom: 10 }}
                    >
                        <Checkbox
                            defaultChecked={todo.fields.Done === true}
                            isChecked={todo.fields.Done === true}
                            onChange={(e) => {
                                const checked = e.target.checked;
                                // Immer 사용.
                                const updatedTodo = produce(
                                    todo,
                                    (nextTodo) => {
                                        nextTodo.fields.Done = checked;
                                    }
                                );
                                updateTodo(updatedTodo);
                            }}
                        />
                        <Editable
                            defaultValue={todo.fields.Name}
                            onChange={(nextValue) => {
                                debouncingInput(todo, nextValue)
                            }}
                        >
                            <EditablePreview />
                            <EditableInput />
                        </Editable>

                        <Button
                            size={'xs'}
                            onClick={() => {
                                deleteTodo(todo.id);
                            }}
                        >
                            삭제
                        </Button>
                    </div>
                );
            })}
            <Button
                colorScheme="blackAlpha"
                onClick={() => {
                    addNewTodo();
                }}
            >
                Add new
            </Button>
        </div>
    );
};

const Wrapper = () => {
    return (
        <div>
            <Test />
            {/* <Test /> */}
        </div>
    );
};

export default Wrapper;

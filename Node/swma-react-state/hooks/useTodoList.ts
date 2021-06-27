// Hook 인식을 위해 반드시 이름에 use 붙여야 함

import axios from 'axios';
import { useMemo } from 'react';
import { useMutation, useQuery, useQueryClient } from 'react-query';
import { useRecoilValue } from 'recoil';
import { Record } from '../interfaces';
import { doFilteringState } from '../store';

// CRUD
const useTodoList = () => {
    // 이걸 잘 쓰는게 중요
    const queryClient = useQueryClient();
    const refresh = () => {
        // "todo-list" key에 cashing된 query를 invalid 처리해서 refresh
        queryClient.invalidateQueries(["todo-list"]);
    }


    // get, R
    // react-query, 알아서 cashing
    const { data, isLoading, isSuccess } = useQuery('todo-list', () => {
        return axios.get('/api/todo');
    });

    // post, C
    // post delete 등 cashing이 필요 없는 request에 대해
    const createMutation = useMutation(() => {
        return axios.post('/api/todo', {
            fields: {
                Name: 'A New Todo Thing',
                Done: false,
            }
        },);
    },
    {
        // React Query의 핵심
        onSuccess: () => {
            refresh();
        },
    });
    
    // delete, D
    // post delete 등 cashing이 필요 없는 request에 대해
    // Recoil과 협력으로 delete 선택 시 바로 UI에 반영되도록 하는 스킬도 있음.
    const deleteMutation = useMutation((id: string) => {
        return axios.delete(`/api/todo/?id=${id}`);
    },
    {
        // React Query의 핵심
        onSuccess: () => {
            refresh();
        },
    });

    // update, U
    // 꽤 까다롭.
    const updateMutation = useMutation((record: Record) => {
        // patch : 업데이트용 메소드
        return axios.patch(`/api/todo/?id=${record.id}`, record);
    },
    {
        // React Query의 핵심
        onSuccess: () => {
            refresh();
        },
    });

    const doFiltering = useRecoilValue(doFilteringState)
    const todoList = useMemo(() => {
        const arr = data ? (data.data.records as Record[]) : []
        if (doFiltering) {
            return arr.filter((row) => !row.fields.Done);
        } else {
            return arr;
        }
    }, [data, doFiltering])

    return {
        todoList: todoList,
        isLoading: isLoading,
        addNewTodo: createMutation.mutate,
        updateTodo: updateMutation.mutate,
        deleteTodo: deleteMutation.mutate
    };
};

export default useTodoList;

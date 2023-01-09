import { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { checkGQL, getTitle, postRoot } from "../api";
import { useQuery as useGQLQuery, gql } from "@apollo/client";

export default function MainScreen() {
  const [gqlResult, setGqlResult] = useState();

  const queryClient = useQueryClient();

  /** useQuery: 조회 */
  const query = useQuery("root", getTitle);

  /** GQL */
  const GET_TODOS = gql`
    query GetTodos {
      allTodos {
        id
        title
        content
        done
      }
    }
  `;

  /** useQuery: GQL ToDo 조회 */
  const { loading, error, data } = useGQLQuery(GET_TODOS);

  /** useMutation: 생성 / 업데이트 / 삭제 */
  const mutation = useMutation(postRoot, {
    onMutate: () => {
      /** mutationFn이 실행되기 전에 실행 */
    },
    onSuccess: () => {
      /** mutationFn이 성공한 후 실행 */

      // 기존 쿼리를 무효화시키고 데이터를 새로 조회
      queryClient.invalidateQueries("root");
    },
    onError: () => {
      /** mutationFn이 실패한 후 실행 */
    },
    onSettled: () => {
      /** 성공 실패 여부 관계없이 실행 */
    },
  });

  console.log(data);

  return (
    <div>
      <span>{JSON.stringify(query.data)}</span>

      <button
        onClick={() => {
          mutation.mutate({ hello: "Server" });
        }}
      >
        Update Title
      </button>
      <br />
      <span>{JSON.stringify(gqlResult)}</span>
      <button
        onClick={() => {
          checkGQL().then((result) => setGqlResult(result));
        }}
      >
        Check GQL
      </button>
      <br />
      <span>GQL</span>
      <div>
        {data.allTodos.map(
          ({
            id,
            title,
            content,
            done,
          }: {
            id: number;
            title: string;
            content: string;
            done: boolean;
          }) => (
            <div key={`${id}_todo`}>
              <h3>{title}</h3>
              <br />
              <span>{content}</span>
              <br />
              <span>{done ? `undone` : `done`}</span>
            </div>
          )
        )}
      </div>
    </div>
  );
}

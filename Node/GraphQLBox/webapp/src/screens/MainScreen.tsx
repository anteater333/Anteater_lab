import { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { checkGQL, getTitle, postRoot } from "../api";

import Button from "react-bootstrap/Button";

import "./MainScreen.scss";
import ToContainer from "../components/ToContainer";

export default function MainScreen() {
  const [gqlResult, setGqlResult] = useState();

  const queryClient = useQueryClient();

  /** useQuery: 조회 */
  const query = useQuery("root", getTitle);

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

  return (
    <>
      <div className="main-header">
        <span className="main-title">{query.data}</span>
        {/* 
        <br />

        <Button
          onClick={() => {
            mutation.mutate({ hello: "Server" });
          }}
        >
          Update Title
        </Button>
        <br />
        <br />
        <span>{JSON.stringify(gqlResult)}</span>
        <br />
        <Button
          onClick={() => {
            checkGQL().then((result) => setGqlResult(result));
          }}
        >
          Check GQL
        </Button> */}
      </div>
      <ToContainer />
    </>
  );
}

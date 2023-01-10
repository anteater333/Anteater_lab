import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";
import Placeholder from "react-bootstrap/Placeholder";
import Modal from "react-bootstrap/Modal";
import Form from "react-bootstrap/Form";

import gql from "graphql-tag";
import { useMutation, useQuery } from "@apollo/client";
import { useState } from "react";

export default function ToContainer() {
  const [isInputMode, setIsInputMode] = useState<boolean>(false);
  const [isTodoInput, setIsTodoInput] = useState<boolean>(false);

  const [newTitle, setNewTitle] = useState<string>("");
  const [newContent, setNewContent] = useState<string>("");
  const [newCost, setNewCost] = useState<number>(0);

  /** GQL */
  const GET_TODOS = gql`
    query GetTodos {
      allTodos {
        id
        title
        content
        createdAt
        done
        doneAt
      }
    }
  `;

  const GET_TOBUYS = gql`
    query GetTobuys {
      allTobuys {
        id
        title
        content
        cost
        createdAt
        bought
        boughtAt
      }
    }
  `;

  const CREATE_TODO = gql`
    mutation TodoCreate($input: TodoCreateInput!) {
      todoCreate(input: $input) {
        todo {
          title
          content
        }
      }
    }
  `;
  const CREATE_TOBUY = gql`
    mutation TobuyCreate($input: TobuyCreateInput!) {
      tobuyCreate(input: $input) {
        tobuy {
          title
          content
          cost
        }
      }
    }
  `;

  const DONE_TODO = gql`
    mutation TodoSetDone($input: TodoUpdateDoneInput!) {
      todoUpdateDone(input: $input) {
        todo {
          id
          done
        }
      }
    }
  `;

  const UNDONE_TODO = gql`
    mutation TodoSetUndone($input: TodoUpdateUndoneInput!) {
      todoUpdateUndone(input: $input) {
        todo {
          id
          done
        }
      }
    }
  `;

  const BOUGHT_TOBUY = gql`
    mutation TobuySetBought($input: TobuyUpdateBoughtInput!) {
      tobuyUpdateBought(input: $input) {
        tobuy {
          id
          bought
        }
      }
    }
  `;

  const UNBOUGHT_TOBUY = gql`
    mutation TobuySetUnbought($input: TobuyUpdateUnboughtInput!) {
      tobuyUpdateUnbought(input: $input) {
        tobuy {
          id
          bought
        }
      }
    }
  `;

  /** useQuery: GQL ToDo 조회 */
  const {
    loading: todoLoading,
    error: todoError,
    data: todoData,
    refetch: refetchTodo,
  } = useQuery(GET_TODOS);
  const {
    loading: tobuyLoading,
    error: tobuyError,
    data: tobuyData,
    refetch: refetchTobuy,
  } = useQuery(GET_TOBUYS);

  const [
    createTodo,
    {
      data: dataCreateTodo,
      loading: loadingCreateTodo,
      error: errorCreateTodo,
    },
  ] = useMutation(CREATE_TODO);
  const [
    createTobuy,
    {
      data: dataCreateTobuy,
      loading: loadingCreateTobuy,
      error: errorCreateTobuy,
    },
  ] = useMutation(CREATE_TOBUY);
  const [
    setDoneTodo,
    { data: dataDoneTodo, loading: loadingDoneTodo, error: errorDoneTodo },
  ] = useMutation(DONE_TODO);
  const [
    setUndoneTodo,
    {
      data: dataUndoneTodo,
      loading: loadingUndoneTodo,
      error: errorUndoneTodo,
    },
  ] = useMutation(UNDONE_TODO);
  const [
    setBoughtTobuy,
    {
      data: dataBoughtTobuy,
      loading: loadingBoughtTobuy,
      error: errorBoughtTobuy,
    },
  ] = useMutation(BOUGHT_TOBUY);
  const [
    setUnboughtTobuy,
    {
      data: dataUnboughtTobuy,
      loading: loadingUnboughtTobuy,
      error: errorUnboughtTobuy,
    },
  ] = useMutation(UNBOUGHT_TOBUY);

  return (
    <div className="main-to-container">
      <div className="main-to-area">
        <div className="main-to-title">To Do</div>
        <div className="main-to-list-scroll">
          <div className="main-to-list">
            {!todoLoading ? (
              todoData ? (
                todoData.allTodos.map(
                  ({
                    id,
                    title,
                    content,
                    done,
                    createdAt,
                    doneAt,
                  }: {
                    id: number;
                    title: string;
                    content: string;
                    done: boolean;
                    createdAt: string;
                    doneAt: string;
                  }) => (
                    <Card className="main-to-card" key={`${id}_todo`}>
                      <Card.Body>
                        <Card.Title>{`#${id} ${title}`}</Card.Title>
                        <Card.Subtitle className="mb-2 text-muted">
                          {`❎${createdAt}`}
                        </Card.Subtitle>
                        {doneAt ? (
                          <Card.Subtitle className="mb-2 text-muted">
                            {`✅${doneAt}`}
                          </Card.Subtitle>
                        ) : undefined}
                        <Card.Text>{content}</Card.Text>
                      </Card.Body>
                      <Card.Footer>
                        <Button>{done ? "Undone" : "Done"}</Button>
                      </Card.Footer>
                    </Card>
                  )
                )
              ) : (
                <div>데이터가 없다.</div>
              )
            ) : (
              <Card className="main-to-card">
                <Card.Body>
                  <Placeholder as={Card.Title} animation="wave">
                    <Placeholder xs={6} />
                  </Placeholder>
                  <Placeholder as={Card.Text} animation="wave">
                    <Placeholder xs={7} /> <Placeholder xs={4} />{" "}
                    <Placeholder xs={4} /> <Placeholder xs={6} />{" "}
                    <Placeholder xs={8} />
                  </Placeholder>
                  <Placeholder.Button variant="primary" xs={6} />
                </Card.Body>
              </Card>
            )}
          </div>
        </div>
        <div className="main-to-controller">
          <Button
            onClick={() => {
              setIsInputMode(true);
              setIsTodoInput(true);
            }}
          >
            추가
          </Button>
        </div>
      </div>
      <div className="main-to-area">
        <div className="main-to-title">To Buy</div>
        <div className="main-to-list-scroll">
          <div className="main-to-list">
            {!tobuyLoading ? (
              tobuyData ? (
                tobuyData.allTobuys.map(
                  ({
                    id,
                    title,
                    content,
                    bought,
                    boughtAt,
                    createdAt,
                    cost,
                  }: {
                    id: number;
                    title: string;
                    content: string;
                    done: boolean;
                    cost: number;
                    bought: boolean;
                    createdAt: string;
                    boughtAt: string;
                  }) => (
                    <Card className="main-to-card" key={`${id}_tobuy`}>
                      <Card.Body>
                        <Card.Title>{`#${id} ${title}`}</Card.Title>
                        <Card.Subtitle className="mb-2 text-muted">{`❎${createdAt}`}</Card.Subtitle>
                        {boughtAt ? (
                          <Card.Subtitle className="mb-2 text-muted">
                            {`✅${boughtAt}`}
                          </Card.Subtitle>
                        ) : undefined}
                        <Card.Text>{content}</Card.Text>
                        <Card.Text>{`${cost.toLocaleString()} ₩`}</Card.Text>
                      </Card.Body>
                      <Card.Footer>
                        <Button>{bought ? "Undone" : "Done"}</Button>
                      </Card.Footer>
                    </Card>
                  )
                )
              ) : (
                <div>데이터가 없다.</div>
              )
            ) : (
              <Card className="main-to-card">
                <Card.Body>
                  <Placeholder as={Card.Title} animation="wave">
                    <Placeholder xs={6} />
                  </Placeholder>
                  <Placeholder as={Card.Text} animation="wave">
                    <Placeholder xs={7} /> <Placeholder xs={4} />{" "}
                    <Placeholder xs={4} /> <Placeholder xs={6} />{" "}
                    <Placeholder xs={8} />
                  </Placeholder>
                  <Placeholder.Button variant="primary" xs={6} />
                </Card.Body>
              </Card>
            )}
          </div>
        </div>
        <div className="main-to-controller">
          <Button
            onClick={() => {
              setIsInputMode(true);
              setIsTodoInput(false);
            }}
          >
            추가
          </Button>
        </div>
      </div>
      <Modal show={isInputMode} onHide={() => setIsInputMode(false)}>
        <Modal.Header>
          <span style={{ fontWeight: "bold" }}>
            {isTodoInput ? `➕ To Do` : `➕ To Buy`}
          </span>
        </Modal.Header>
        <Modal.Body>
          <div className="to-modal-input-container">
            <label>Title</label>
            <Form.Control
              type="text"
              value={newTitle}
              onChange={(e) => setNewTitle(e.target.value)}
            />
          </div>
          <div className="to-modal-input-container">
            <label>Content</label>
            <Form.Control
              type="text"
              value={newContent}
              onChange={(e) => setNewContent(e.target.value)}
            />
          </div>
          {!isTodoInput ? (
            <div className="to-modal-input-container">
              <label>Cost</label>
              <Form.Control
                type="number"
                value={newCost}
                onChange={(e) => {
                  setNewCost(parseInt(e.target.value));
                }}
              />
            </div>
          ) : undefined}
        </Modal.Body>
        <Modal.Footer>
          <Button
            disabled={loadingCreateTobuy || loadingCreateTodo}
            onClick={(e) => {
              e.preventDefault();
              let queryPromise;
              if (isTodoInput)
                queryPromise = createTodo({
                  variables: {
                    input: { title: newTitle, content: newContent },
                  },
                });
              else
                queryPromise = createTobuy({
                  variables: {
                    input: {
                      title: newTitle,
                      content: newContent,
                      cost: newCost,
                    },
                  },
                });

              queryPromise
                .then((result) => {
                  setNewContent("");
                  setNewTitle("");
                  setNewCost(0);
                  setIsInputMode(false);
                  refetchTobuy();
                  refetchTodo();
                })
                .catch((err) => {
                  console.error(err);
                });
            }}
          >
            추가
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
}

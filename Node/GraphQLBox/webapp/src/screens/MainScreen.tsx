import { useMutation, useQuery, useQueryClient } from "react-query";
import { getRoot, postRoot } from "../api";

export default function MainScreen() {
  const queryClient = useQueryClient();

  const query = useQuery("root", getRoot);

  const mutation = useMutation(postRoot, {
    onSuccess: () => {
      queryClient.invalidateQueries("root");
    },
  });

  return (
    <div>
      <span>{JSON.stringify(query.data)}</span>

      <button
        onClick={() => {
          mutation.mutate({ hello: "Server" });
        }}
      >
        BUTTON
      </button>
    </div>
  );
}

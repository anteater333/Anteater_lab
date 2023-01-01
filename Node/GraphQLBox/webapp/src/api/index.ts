export const getRoot = async () => {
  const response = await (await fetch("http://localhost:4321/")).json();
  return response;
};

export const postRoot = async (data: any) => {
  const response = await fetch("http://localhost:4321", {
    method: "POST",
    body: JSON.stringify(data),
    headers: {
      "Content-Type": "application/json",
      "Access-Control-Allow-Origin": "*",
    },
  });

  return await response.json();
};

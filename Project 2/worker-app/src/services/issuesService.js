// get Issues

const getIssues = () => {
  const issues = [
    {
      id: 1,
      title: "issue1",
      state: "solving",
      description: "Ola o meu nome é Joao Carlos Maduro",
      date: "15-2-2020",
      time: "14:20",
    },
    {
      id: 2,
      title: "issue2",
      state: "solved",
      description: "Ola o meu nome é Joao Carlos Maduro",
      date: "21-6-2020",
      time: "23:32",
    },
  ];

  return Promise.resolve(issues);
};
// send Issues

const sendIssue = (issue) => {
  const today = new Date();
  const state = "unsolved";
  const date = `${today.getDay()}-${today.getMonth()}-${today.getFullYear()}`;
  const time = `${today.getHours()}:${today.getMinutes()}`;
  const sendIssue = { ...issue, time, date, state };
  return Promise.resolve({
    ...sendIssue,
    id: Math.floor(Math.random() * (5000 - 3)) + 3,
  });
};

export const issuesService = {
  sendIssue,
  getIssues,
};

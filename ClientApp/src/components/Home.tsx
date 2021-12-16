import * as React from "react";
import { HubConnectionBuilder, HubConnection } from "@microsoft/signalr";
import { message, Input, Button } from "antd";
import { Message, Player } from "../Interfaces/Message";

import "antd/dist/antd.css";
import { InputGroup } from "reactstrap";

export const Home = () => {
  const [connection, setConnection] = React.useState<HubConnection>();
  const [buttonVisible, setButtonVisible] = React.useState<Boolean>(false);
  const [questionText, setQuestionText] = React.useState<string>();
  const [gameInfoRecieved, setGameInfoRecieved] = React.useState<string>("");

  const refer = React.useRef<any>();

  const PlayAgain = (option: boolean) => {
    connection?.invoke("PlayAgain", option);
    setButtonVisible(false);
    refer.current.focus();
  };

  const PlayAgainSelector = () => {
    if (buttonVisible) {
      return (
        <>
          <h1>{questionText}</h1>
          <Button onClick={() => PlayAgain(true)}>Yes</Button>
          <Button onClick={() => PlayAgain(false)}>No</Button>
        </>
      );
    }
  };

  React.useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl("/notifications")
      .withAutomaticReconnect()
      .build();
    setConnection(newConnection);
  }, []);

  React.useEffect(() => {
    if (connection) {
      connection
        .start()
        .then((result) => {
          console.log("Connected!");

          connection.on("ReceiveMessage", (data: string) => {
            setGameInfoRecieved((x) => (x += data + "\n"));
          });

          connection.on("RecieveQuestion", (question: string) => {
            setQuestionText(question);
            setButtonVisible(true);
          });

          connection.on("ClearScreen", () => {
            setGameInfoRecieved("");
          });

          connection.on("RecieveHiscores", (hiscores: Array<Player>) => {
            let scores = hiscores.map(
              (player) =>
                `${player.name} average score: ${player.averageGuesses}`
            );
            setGameInfoRecieved(scores.join("\n"));
          });
        })
        .catch((e: any) => console.log("Connection failed: ", e));
    }
  }, [connection]);

  const SendToServer = async () => {
    console.log(refer.current.state.value);
    try {
      await connection?.invoke("UserInput", refer.current.state.value);
    } catch (err) {
      console.error(err);
    }

    refer.current.state.value = "";
  };

  return (
    <>
      <Input.TextArea
        value={gameInfoRecieved}
        readOnly={true}
        style={{ width: "100%", height: "500px" }}
      ></Input.TextArea>
      <Input
        ref={refer}
        onKeyDown={(e) => {
          if (e.key == "Enter") {
            SendToServer();
          }
        }}
      />
      {PlayAgainSelector()}
    </>
  );
};
export default Home;

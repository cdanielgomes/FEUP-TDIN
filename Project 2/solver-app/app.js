import React, { Component } from "react"; // import from react

import { Window, App, Text } from "proton-native"; // import the proton-native components

export default class Example extends Component {
  render() {
    // all Components must have a render method
    return (
      <App>
        {/* you must always include App around everything */}
        <Window style={{ width: 300, height: 300, backgroundColor: "white" }}>
          <Text>Habemus App irmão</Text>
        </Window>
      </App>
    );
  }
}

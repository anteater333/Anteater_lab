import React, {useEffect} from 'react';
import Axios from 'axios'
import { useDispatch, useSelector } from 'react-redux'
import { saveMessage } from '../_actions/message_actions'
import { List, Icon, Avatar } from 'antd';
import Message from './Sections/Message'
import Card from './Sections/Card'

function Chatbot() {

    const dispatch = useDispatch()
    const messagesFromRedux = useSelector(state => state.message.messages)

    // React-tech
    // DOM을 렌더링한 후에 react에게 이 컴포넌트에서 이거를 해야돼! 이렇게 말하는 것을 useEffect에다 정의
    useEffect(() => {
        eventQuery('welcomeToMyWebsite')
    }, [])

    const textQuery = async (text) => {

        // 1. need to take care of the message I sent
        let conversation = {
            who: 'user',    // which is me
            content: {
                text: { // 왜 이런 형태인지는 Postman에서 어떤 response를 받았는지 생각해보면 됨.
                    text: text
                }
            }
        }

        dispatch(saveMessage(conversation))
        
        // 2. we need to take care of the message Chatbot sent
        const textQueryVariables = {
            text
        }

        try {   // try to send to the textQuery route
            const response = await Axios.post('/api/dialogflow/textQuery', textQueryVariables )

            for(let content of response.data.fulfillmentMessages) {
                let conversation = {
                    who: 'bot',
                    content: content
                }

                dispatch(saveMessage(conversation))
            }

        } catch (error) {
            conversation = {
                who: 'bot',
                content: {
                    text: {
                        text: " Error just occured. something's wrong."
                    }
                }
            }

            
            dispatch(saveMessage(conversation))
        }

    }

    const eventQuery = async (event) => {
        
        // 2. we need to take care of the message Chatbot sent
        const eventQueryVariables = {
            event
        }

        try {   // try to send to the textQuery route
            const response = await Axios.post('/api/dialogflow/eventQuery', eventQueryVariables )

            for (let content of response.data.fulfillmentMessages) {
                let conversation = {
                    who: 'bot',
                    content: content
                }

                dispatch(saveMessage(conversation))
            }

        } catch (error) {
            let conversation = {
                who: 'bot',
                content: {
                    text: {
                        text: " Error just occured. something's wrong."
                    }
                }
            }

            
            dispatch(saveMessage(conversation))
        }

    }
    
    const keyPressHandler = (e) => {
        if (e.key === "Enter") {

            if (!e.target.value) {
                return alert('Type something first.')
            }

            // we'll send req to textQuery route
            textQuery(e.target.value)

            e.target.value = "";

        }
    }

    const renderCards = (cards) => {
        return cards.map((card, i) => <Card key={i} cardInfo={card.structValue} />)
    }

    const renderOneMessage = (message, i) => {
        console.log('message', message)

        // condition to seperate message kinds
        // template for normal text
        if (message.content && message.content.text && message.content.text.text) {
            return <Message who={message.who} text={message.content.text.text} />
        }
        // template for card message
        else if (message.content && message.content.payload.fields.card) {

            const AvatarSrc = message.who === 'bot' ? <Icon type="robot" /> : <Icon type="smile" />

            return <div>
                <List.Item style={{ padding: '1rem' }}>
                    <List.Item.Meta
                        avatar={<Avatar icon={AvatarSrc} />}
                        title={message.who}
                        description={renderCards()}
                    />
                </List.Item>
            </div>
        }

    } 

    const renderMessage = (returnedMessages) => {
        if(returnedMessages) {
            return returnedMessages.map((message, i) => {
                return renderOneMessage(message, i);
            })
        } else {
            return null;
        }
    }

    return (
        <div style={{
            height: 700, width: 700,
        border: '3px solid black', borderRadius: '7px'
        }}>
            <div style={{ height: 644, width: '100%', overflow: 'auto' }}>
                {renderMessage(messagesFromRedux)}
            </div>
            

            <input
                style={{
                    margin: 0, width: '100%', height: 50,
                    borderRadius: '4px', padding: '5px', fontSize: '1rem'
                }}
                placeholder="Send a message..."
                onKeyPress={keyPressHandler}
                type="text"
            />

        </div>
    )
}

export default Chatbot
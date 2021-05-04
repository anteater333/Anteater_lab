import { combineReducers } from 'redux';    // combineReducers 다양한 자원에 대한 reducer를 rootReducer로 통합
import user from './user_reducer';

const rootReducer = combineReducers({
    user,
})

export default rootReducer;
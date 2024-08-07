import axios from 'axios';
import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { auth } from '../_actions/user_action';

// eslint-disable-next-line import/no-anonymous-default-export
export default function (SpecificComponent, option, adminRoute = null) {
    // option
    // null => 아무나 출입이 가능한 페이지
    // true => 로그인한 유저만 출입이 가능한 페이지
    // false => 로그인한 유저는 출입 불가능한 페이지

    //adminRoute => 어드민 유저만 들어갈 수 있는 페이지 (boolean, 기본 null)

    function AuthenticationCheck(props) {
        const dispatch = useDispatch();

        useEffect(() => {
            dispatch(auth()).then((response) => {
                console.log(response);

                // 로그인 하지 않은 상태
                if (!response.payload.isAuth) {
                    if (option) {
                        props.history.push('/login');
                    }
                }
                // 로그인 한 상태
                else {
                    if (adminRoute && !response.payload.isAdmin) {
                        alert('관리자만 접근 가능한 페이지입니다.');
                        props.history.push('/');
                    } else if (option === false) {
                        props.history.push('/');
                    }
                }
            });
        }, []);

        return <SpecificComponent />;
    }
    return AuthenticationCheck;
}

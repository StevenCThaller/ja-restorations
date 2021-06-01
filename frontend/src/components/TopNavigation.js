// import React, { useEffect, useState } from 'react'
// import { connect } from 'react-redux';
// import { withRouter, NavLink } from 'react-router-dom';
// import { decryptEmail } from '../actions/userActions';
// const login = state => {

// }


// const TopNavigation = props => {
//     const { auth, user } = props;
//     let loginText = "Log In";
//     let loginLink = "/login";
//     if(auth.isAuthenticated){
//         loginText = "Log Out";
//         loginLink = "/logout";
//     }

//     return (
//         <>
//             <nav>
//                 <div>
//                     <NavLink exact to="/">JA Restorations</NavLink>
//                     <p>Welcome, { user.email }</p>
//                 </div>
//                 <div>
//                         <p><NavLink exact to="/">Home</NavLink></p>
//                         <p><NavLink exact to={loginLink}>{loginText}</NavLink></p>
//                 </div>
//             </nav>
//         </>
//     )
// }

// const mapStateToProps = state => {
//     return { 
//         auth: state.auth,
//         user: state.user
//     };
// }


// export default withRouter(connect(mapStateToProps)(TopNavigation));

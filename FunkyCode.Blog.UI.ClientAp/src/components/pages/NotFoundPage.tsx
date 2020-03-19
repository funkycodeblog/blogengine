import React, { Component } from 'react';
import { connect } from 'react-redux';

import { IAppState } from '../../redux/Store';


interface Props {
    
}

interface State  {
    
}

class NotFoundPage extends Component<Props, State>  {

    

    render() {

        
        return <div style={{textAlign:'center', position: 'absolute', top: '30%', left: '38%'}}>    
            Page not found     
        </div>
    }
}

const mapStateToProps = () => {
    return {
     
    };
};

// const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, AnyAction>) => {
//     return {
    
//     };
// };

export default connect(mapStateToProps/* mapDispatchToProps*/)(NotFoundPage);
  
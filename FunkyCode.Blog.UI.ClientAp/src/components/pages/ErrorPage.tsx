import React, { Component } from 'react';
import { connect } from 'react-redux';
import { ReactComponent as UnhappySmile } from '../../assets/icons/img_empty.svg'
import { IAppState } from '../../redux/Store';
import { ErrorInfo } from '../../model/ErrorInfo';
import { IFunkyState } from '../../redux/State';
import { isNullOrUndefined } from 'util';
import { Typography } from '@material-ui/core';
import { FunkyButton } from '../UIComponents/Button';


interface Props {
    errorInfo? : ErrorInfo
}

interface State  {
    
}

class ErrorPage extends Component<Props, State>  {
   

    render() {

        const { errorInfo } = this.props;
 
        const message = isNullOrUndefined(errorInfo) ? '' : errorInfo.message;

        return <div style={{textAlign:'center', position: 'absolute', top: '30%', left: '35%', width: '30%'}}>

        <div style={{display: 'flex', flexDirection: 'column', justifyContent: 'space-evenly' }}>
                
                    <div>
                    <UnhappySmile/>
                    </div>
                    
                    <div style={{height: '20px'}} />
              
                    <div>
                    <Typography variant="h4">Error</Typography>
                    </div>
          
                    <div style={{height: '20px'}} />
        
        
                    <div>
                    <Typography   >{message}</Typography>
                    </div>
        
                    <div style={{height: '20px'}} />
        
                    <div>
                    <FunkyButton buttonType="primary" title="Restart"  onClickEvent={this.handleClick.bind(this)} />
                    </div>
        
                
                </div>
        
                </div>
            }

            handleClick(): void {
                window.location.replace("/");
            }
    }


const mapStateToProps = (store: IAppState) => {

    const state = store.funkyState as IFunkyState;

    return {
        errorInfo: store.funkyState.errorInfo
    };
};

// const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, AnyAction>) => {
//     return {
    
//     };
// };

export default connect(mapStateToProps,)(ErrorPage);
  
import React, { Component } from 'react';
import { IAppState } from '../../redux/Store';
import { ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';
import { connect } from 'react-redux';
import { InputBase } from '@material-ui/core';

interface Props {
    dispatch: ThunkDispatch<any, any, AnyAction>;
    onValueEntered: (value: string) => void;
}

interface State  {

    searchValue : string;
    
}

class SearchInput extends Component<Props, State>  {

        state = {searchValue: ''};
        
        ENTER_KEY_CODE : number = 13;

        render() {
            
            const combined = textField;
            return <div><InputBase inputProps={{ style: {textAlign: 'center'} }} value = {this.state.searchValue}  style={combined} placeholder='Search' onChange={this.onChange.bind(this)} onKeyUp={this.onKeyUp.bind(this)} > 
            </InputBase>
             
            </div>
    
        }

        onChange = (e: React.ChangeEvent<HTMLInputElement>) => {
            const newValue = e.target.value;
            this.setState({searchValue: newValue});
        }

        onKeyUp = (e : React.KeyboardEvent<HTMLInputElement>) => {
           const isEnter : boolean = e.keyCode === this.ENTER_KEY_CODE;
           if (isEnter)
           {
                this.props.onValueEntered(this.state.searchValue);
           }
        }
    
    }



const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, AnyAction>) => {
     return {
         dispatch
     };
 };
  
const mapStateToProps = (store: IAppState) => {
    
    // var state : IFunkyState = store.funkyState;
  
};

export default connect(mapStateToProps, mapDispatchToProps)(SearchInput);

const textField = {

    border: '1px solid #E5E5E5',
    color: 'white',
    height: '40px',
    paddingBottom: 0,
    fontSize: '18px',
    fontWeight: 'normal' as 'normal',
    marginTop: 0,
    borderRadius: '100px',
    zIndex: 1000

}

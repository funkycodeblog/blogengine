import React, { Component } from 'react';
import { ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';
import { connect } from 'react-redux';
import { TextField } from 'material-ui-formik-components/TextField'
import * as yup from 'yup';
import { Formik, FormikProps, Form, Field, FormikErrors } from 'formik';
import { Typography, Dialog, DialogTitle, DialogActions, Button, DialogContent } from '@material-ui/core';
import { Spacer } from '../Spacer';
import { FunkyButton } from '../UIComponents/FunkyButton';
import { isNullOrUndefined } from 'util';
import { ContactDataModel } from '../../model/ContactDataModel';
import { postContactMessage, subscribeAction } from '../../redux/Thunks';
import { IAppState } from '../../redux/Store';
import { IFunkyState } from '../../redux/State';
import { FunkyMessage } from '../UIComponents/FunkyMessage';
import { BlogEnginePaths } from '../../config/BlogEngineSettings';
import { Redirect } from 'react-router-dom';
import { resetUiStateAction } from '../../redux/Actions';
import { SubscribeDataModel } from '../../model/SubscribeDataModel';
import { SubscribeDataActionType } from "../../model/SubscribeDataActionType";
import { SubscribeDto } from '../../model/SubscribeDto';

interface Props {
    dispatch: ThunkDispatch<any, any, AnyAction>;
    isContactMessagePosted: boolean;
}

interface State  {
    isMsgClosed: boolean;
    action: SubscribeDataActionType
}

const validationSchema = yup.object().shape<SubscribeDataModel>({
    username: yup
      .string()
      .max(30)
      .required(),
    email: yup
      .string()
      .email()
      .required(),
  });

class SubscribePage extends Component<Props & FormikProps<ContactDataModel>, State>  {

    initialValues: SubscribeDataModel = { username: '', email: ''};

    state : State = { isMsgClosed: false, action: 'unknown'  }

    componentWillMount()
    {

      this.props.dispatch(resetUiStateAction());

    }

    render() {

        if (this.state.isMsgClosed)
        {
            return <Redirect to={ BlogEnginePaths.MainPath} />
        }

        return <div style={{width: '600px'}}>
        
        <Spacer height={30} />
        <Typography variant="h4">Subscribe</Typography>

        <Formik
        initialValues={ this.initialValues} 
        onSubmit={this.onSubmit.bind(this)}         
        validationSchema={validationSchema}
        >
        {() => (
          
          <Form>
          <Field name="username" label="Username" component={TextField}  variant="filled" />
          <Field name="email" label="Email" component={TextField}  variant="filled"/>
          <Spacer height={20} />
       
          <div>
            <span>
            <FunkyButton buttonType="border" title="Register" onClickEvent={() => {this.subscribe()  }} submit />
            </span>

            <span>  
              <FunkyButton buttonType="border" title="Unregister" onClickEvent={() => { this.unsubscribe()}} submit /> 
            </span>



          </div>
         
          
          </Form>

        )}
      </Formik>

      <FunkyMessage title="Funky Code" message="Message was sent!" isOpen={this.props.isContactMessagePosted} onClose={this.onClose.bind(this)} />

      </div>
    }

    subscribe()
    {
        this.setState({...this.state, action: 'subscribe' });
    }

    unsubscribe()
    {
      this.setState({...this.state, action: 'unsubscribe' });
    }

    onSubmit(data: SubscribeDataModel) {

        const subscribeDto : SubscribeDto = {...data, action: this.state.action};

        this.props.dispatch(subscribeAction(subscribeDto));
    }

    onClose() {
        this.setState({isMsgClosed: true});
    }

}


const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, AnyAction>) => {
     return {
         dispatch
     };
 };
  
const mapStateToProps = (store: IAppState) => {

  const state: IFunkyState = store.funkyState;

  return {
      isContactMessagePosted: state.isContactMessagePosted
  };

};

export default connect(mapStateToProps, mapDispatchToProps)(SubscribePage);
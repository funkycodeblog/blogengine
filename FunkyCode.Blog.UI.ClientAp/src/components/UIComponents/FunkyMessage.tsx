import React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import { FunkyButton } from './FunkyButton';

interface IProps
{
    title: string;
    message: string;
    isOpen: boolean;
    onClose: () => void;
}

export const FunkyMessage: React.SFC<IProps> = (props) => {
  
  return (
    <div style={{width: '500px'}} >
      <Dialog
        open={props.isOpen}
        onClose={props.onClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">{props.title}</DialogTitle>
        <DialogContent style={{width: '500px'}}>
          <DialogContentText id="alert-dialog-description">
            {props.message}
          </DialogContentText>
        </DialogContent>
        <DialogActions>
            <FunkyButton buttonType="border" title="Ok" onClickEvent={props.onClose} /> 
        </DialogActions>
      </Dialog>
    </div>
  );
}
using System.Threading.Tasks;

namespace FunkyCode.Blog
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery
    {
         Task <TResult> Handle(TQuery query);
    }
}


/*
################################################################################
#                                                                              #
# COPYRIGHT Ericsson 2018                                                      #
#                                                                              #
# The copyright to the computer program(s) herein is the property of Ericsson  #
# AB. The programs may be used and/or copied only with written permission      #
# from Ericsson AB. or in accordance with the terms and conditions stipulated  #
# in the agreement/contract under which the program(s) have been supplied.     #
#                                                                              #
################################################################################
*/

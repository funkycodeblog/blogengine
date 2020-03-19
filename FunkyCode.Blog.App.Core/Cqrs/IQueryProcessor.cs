using System.Threading.Tasks;

namespace FunkyCode.Blog
{
    public interface IQueryProcessor
    {
        Task <TResult> Process<TQuery, TResult>(TQuery query) where TQuery : IQuery;
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

import numpy as np


def triangulate(x, Ps):
    '''
        Ref: http://www.cs.cmu.edu/~16385/s17/Slides/11.4_Triangulation.pdf
    :param x: matched 2d point with size Nviews x 2
    :param Ps: camera matrix with size Nviews x 3 x 4
    :return: 3d point
    '''
    nviews = x.shape[1]
    A = np.zeros((2 * nviews, 4))
    for i in range(nviews):
        A[2 * i, :] = x[i, 1] * Ps[i, 2, :] - Ps[i, 1, :]
        A[2 * i + 1, :] = Ps[i, 0, :] - x[i, 0] * Ps[i, 2, :]
    u, s, v = np.linalg.svd(A)
    X = v[-1, :]
    X = v[-1, :] / v[-1, -1]
    return X[:3]


if __name__ == "__main__":
    # Step 1: read in KRT for multiple cams

    # Step 2: load multi cams

    # Step 3: call openpose for each cams (better assigning different GPU usage)

    # Step 4: format 2D skeletons

    # Step 5: call triangulation to get 3D skeleton

    # Step 6: format 3D skeleton file and save to proper location

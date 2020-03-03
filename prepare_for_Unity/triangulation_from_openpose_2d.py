import os
import sys

import json
from tqdm import tqdm

import numpy as np

def triangulate(x, Ps, num_views):
  '''
    Ref: http://www.cs.cmu.edu/~16385/s17/Slides/11.4_Triangulation.pdf
    :param x: matched 2d point with size Nviews x 2
    :param Ps: camera matrix with size Nviews x 3 x 4
    :return: 3d point
  '''
  nviews = x.shape[1]
  A = np.zeros((num_views * nviews, 4))
  for i in range(nviews):
    A[2 * i, :] = x[i, 1] * Ps[i, 2, :] - Ps[i, 1, :]
    A[2 * i + 1, :] = Ps[i, 0, :] - x[i, 0] * Ps[i, 2, :]
  u, s, v = np.linalg.svd(A)
  X = v[-1, :]
  X = v[-1, :] / v[-1, -1]
  return X[:3]

def main(json_dir, output_dir, calibration_dir):
  json_to_process = list()
  cams = set()

  # Count cameras/views
  for case in os.listdir(json_dir):
    cams.add(int(case[5]))
  num_views = len(cams)

  # Count frames
  for case in os.listdir(json_dir):
    if case[-5:] == '.json' and case [5] == '1':
      json_to_process.append(case.strip().split('_')[1])
  
  # Read camera calibration (KRT-matrix)
  cams_calibration = list()
  for cam_idx in cams:
    cam_matrix_txt = open(os.path.join(calibration_dir, 'Camera' + str(cam_idx) + '.Pmat.cal'), 'r')
    cam_matrix = str()
    for line in cam_matrix_txt.readlines():
      cam_matrix += line.strip() + ' '
    cam_matrix = np.array(cam_matrix.strip().split(' '), dtype=np.float32).reshape(3,4)
    cams_calibration.append(cam_matrix)
  cams_calibration = np.array(cams_calibration)
  
  # Read and optimize 2D keypoints
  for case in tqdm(json_to_process):
    pose_kpd_2d = list()
    for cam_idx in cams:
      curr_json = json.load(open(os.path.join(json_dir, 'Image' + str(cam_idx) + '_' + case) + '_keypoints.json', 'r'))
      pose_kpd_2d_view_json = curr_json['people'][0]['pose_keypoints_2d']
      pose_kpd_2d_view = list()
      for j in range(len(pose_kpd_2d_view_json) // 3):
        pose_kpd_2d_view.append(pose_kpd_2d_view_json[j * 3: (j+1) * 3 - 1])
      pose_kpd_2d.append(pose_kpd_2d_view)
    pose_kpd_2d = np.array(pose_kpd_2d)
    pts3d_all = list()
    for i in range(pose_kpd_2d[0].shape[0]):
      pts2d = pose_kpd_2d[:, i]
      pts3d = triangulate(pts2d, cams_calibration, num_views)
      pts3d_all.append(pts3d)
    # Save to npy for each frame
    np.save(os.path.join(output_dir, case + '_3d_kpds.npy'), np.array(pts3d_all))

if __name__ == "__main__":
  json_dir = '/home/ICT2000/hxiao/Datasets/forHanyuan/bouncing_keypoints2d'
  output_dir = '/home/ICT2000/hxiao/Datasets/forHanyuan/bouncing_keypoints3d'
  calibration_dir = '/home/ICT2000/hxiao/Datasets/forHanyuan/bouncing_calibration/calibration'

  os.makedirs(output_dir, exist_ok=True)
  main(json_dir, output_dir, calibration_dir)
  